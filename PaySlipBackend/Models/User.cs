﻿using System.ComponentModel.DataAnnotations;

namespace PaySlipBackend.Models
{
    public class User
    {
        [Key]

        public string? Email { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set; }

    }

    public class UserDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
