using System.ComponentModel.DataAnnotations;

namespace BlazeIt.Shared.Entities
{
    public class Feedback
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Votes { get; set; } = 0;

        // Navigation property
        public string CreatedById { get; set; }
        public List<User> Voters { get; set; } = new List<User>(); // Users who voted on feedback

        // https://www.youtube.com/watch?v=OeOymtdQagw
    }
}
