using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscussionForum.DTOs;

namespace DiscussionForum.Models
{
    [Table("Forums")]
    public class Forum
    {
        // Properties
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
    }
}
