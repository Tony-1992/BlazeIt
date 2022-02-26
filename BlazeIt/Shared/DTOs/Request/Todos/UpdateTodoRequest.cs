namespace BlazeIt.Shared.DTOs.Request
{
    public class UpdateTodoRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
