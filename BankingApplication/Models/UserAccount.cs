﻿using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
