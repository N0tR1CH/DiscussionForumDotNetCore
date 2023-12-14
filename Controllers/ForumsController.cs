using DiscussionForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.Controllers;

[Route("[controller]")]
[ApiController]
public class ForumsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<ForumsController> _logger;

    public ForumsController(AppDbContext context, ILogger<ForumsController> logger)
    {
        _context = context;
        _logger = logger;
    }
}
