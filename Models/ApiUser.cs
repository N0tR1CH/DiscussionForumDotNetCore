using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApiUser : IdentityUser
{
    [Required]
    public int? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    [MaxLength(50)]
    public override string? Email {  get; set; }
    [Required]
    [MaxLength(20)]
    public string Login { get; set; }
    [Required]
    public DateTime JoiningDate { get; set; }

}