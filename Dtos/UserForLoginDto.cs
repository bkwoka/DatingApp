using System.ComponentModel.DataAnnotations;

namespace DatingApp.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 3,
            ErrorMessage = "You must specify username between 3 and 16 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6, 
            ErrorMessage = "You must specify password between 6 and 255 characters")]
        public string Password { get; set; }
    }
}