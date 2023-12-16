using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiscussionForum.Models
{
    [Table("Posts")]
    public class Post
    {
        // Properties
        [Required]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = default!;

        [MaxLength(2000)]
        public string Description { get; set; } = default!;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        public ICollection<Forum>? Forums { get; set; }

        public ICollection<Photo>? Photos { get; set; }
    }
}
