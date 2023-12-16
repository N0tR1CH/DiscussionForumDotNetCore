using System.ComponentModel.DataAnnotations;

namespace DiscussionForum.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Title { get; set; }
        public string Source { get; set; }
        public int CategoryId { get; set; }
    }
}
