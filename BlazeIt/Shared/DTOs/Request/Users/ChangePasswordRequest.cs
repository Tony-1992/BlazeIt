using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.DTOs.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
    }
}
