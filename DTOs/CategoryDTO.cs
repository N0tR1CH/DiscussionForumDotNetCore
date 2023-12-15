using System.ComponentModel.DataAnnotations;

namespace DiscussionForum.DTOs;

public class CategoryDTO
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string? Name { get; set; }

    [StringLength(2000, ErrorMessage = "Description must be less than 2000 characters")]
    public string? Description { get; set; }
}
