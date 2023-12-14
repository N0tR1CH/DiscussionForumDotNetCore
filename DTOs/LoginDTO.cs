using System.ComponentModel.DataAnnotations;

namespace DiscussionForum.DTOs;

public class LoginDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}
