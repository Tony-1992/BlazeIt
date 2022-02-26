using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.Entities
{
    public class Todo
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public bool Completed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // Navigation property
        public string UserId { get; set; }

    }
}
