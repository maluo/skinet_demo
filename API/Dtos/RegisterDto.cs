using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z].*[a-z])(?=.*[A-Z].*[A-Z])(?=.*\d.*\d)(?=.*\W.*\W)[a-zA-Z0-9\S]{9,}$", 
            ErrorMessage = "This pattern requires at least two lowercase letters, two uppercase letters, two digits, and two special characters. There must be a minimum of 9 characters total, and no white space characters are allowed.")]
        //qW1@xxx
        public string Password { get; set; }
    }
}