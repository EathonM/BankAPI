﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Domain.Entities
{
    public class BankAccount
    {
        public long AccountId { get; set; }
        public decimal Balance { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
