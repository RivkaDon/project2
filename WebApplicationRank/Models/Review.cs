using System.ComponentModel.DataAnnotations;

namespace WebApplicationRank.Models
{
    public class Review
    {
        [Key, Required]
        public string? Name { get; set; }

        public string? Feedback { get; set; }

        [Range(1, 5)]

        public int Score { get; set; }

        public DateTime DateTime { get; set; }
    }
}
