namespace WebAPIs.Models.Requests
{
    public class UpdateMessageRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? Active { get; set; }
    }
}