using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;


        // Navigation properties
        public List<Todo> Todos { get; set; }
        public List<Feedback> VotedOn { get; set; } = new List<Feedback>(); // Feedback Users Voted On

    }
}
