using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly string _connectionString;
    private readonly IAmazonSimpleNotificationService _snsClient;

    public BankAccountController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.USEast2); // Specify your region
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(long accountId, decimal amount)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            // Check current balance
            string sql = "SELECT balance FROM accounts WHERE id = @accountId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@accountId", accountId);

            object result = await command.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                decimal currentBalance = (decimal)result;
                if (currentBalance >= amount)
                {
                    // Update balance
                    sql = "UPDATE accounts SET balance = balance - @amount WHERE id = @accountId";
                    command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@accountId", accountId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        // After a successful withdrawal, publish a withdrawal event to SNS
                        WithdrawalEvent withdrawalEvent = new WithdrawalEvent(amount, accountId, "SUCCESSFUL");
                        string eventJson = withdrawalEvent.ToJson();
                        string snsTopicArn = "arn:aws:sns:YOUR_REGION:YOUR_ACCOUNT_ID:YOUR_TOPIC_NAME";

                        PublishRequest publishRequest = new PublishRequest
                        {
                            Message = eventJson,
                            TopicArn = snsTopicArn
                        };

                        PublishResponse publishResponse = await _snsClient.PublishAsync(publishRequest);
                        return Ok("Withdrawal successful");
                    }
                    else
                    {
                        // In case the update fails for reasons other than a balance check
                        return BadRequest("Withdrawal failed");
                    }
                }
                else
                {
                    // Insufficient funds
                    return BadRequest("Insufficient funds for withdrawal");
                }
            }
            else
            {
                return BadRequest("Account not found");
            }
        }
    }

    public class WithdrawalEvent
    {
        public decimal Amount { get; set; }
        public long AccountId { get; set; }
        public string Status { get; set; }

        public WithdrawalEvent(decimal amount, long accountId, string status)
        {
            Amount = amount;
            AccountId = accountId;
            Status = status;
        }

        // Convert to JSON String
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
