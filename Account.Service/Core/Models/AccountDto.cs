﻿using Account.Service.Core.Enums;

namespace Account.Service.Core.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public OperationType OperationType { get; set; }
    }
}
