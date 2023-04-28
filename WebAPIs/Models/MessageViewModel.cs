namespace WebAPIs.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
