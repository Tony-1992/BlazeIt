using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.DTOs.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
