namespace WebAPI.Models
{
    public class RequestEditMessage
    {
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public bool Sent { get; set; }
    }
}
