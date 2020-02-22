using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DatingApp.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 3,
            ErrorMessage = "You must specify username between 3 and 16 characters")]
        public string Username { get; set; }
        [StringLength(24, MinimumLength = 6, 
            ErrorMessage = "You must specify password between 6 and 24 characters")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}