using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.DTOs.Request
{
    public class CreateTodoRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
