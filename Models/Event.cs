using System.ComponentModel.DataAnnotations;

namespace CollegeInfoPortal.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? Location { get; set; }
    }
}
