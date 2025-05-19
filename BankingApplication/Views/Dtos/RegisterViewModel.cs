﻿using System;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
