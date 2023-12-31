using System.ComponentModel.DataAnnotations;

namespace DiscussionForum.DTOs;

public class RegisterDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
