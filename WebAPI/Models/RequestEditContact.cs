namespace WebAPI.Models
{
    public class RequestEditContact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        public DateTime? LastDate { get; set; }
    }
}
