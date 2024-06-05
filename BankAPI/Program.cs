using Banking.Account;
using FastEndpoints;
using FastEndpoints.Security;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false)
                .Build();

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting up");

builder.Configuration.AddConfiguration(config);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFastEndpoints()
    .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization()
    .AddSwaggerGen();

List<System.Reflection.Assembly> mediatrAssemblies = [typeof(Program).Assembly];
builder.Services.AddBankAccountModule(builder.Configuration, logger, mediatrAssemblies);


builder.Services.AddMediatR(cfg => 
cfg.RegisterServicesFromAssemblies(mediatrAssemblies.ToArray()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwaggerUI();
}

app.UseAuthorization()
    .UseAuthorization();

app.UseFastEndpoints()
    .UseSwagger()
    .UseSwaggerUI();

app.MapControllers();

app.Run();
