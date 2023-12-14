using DiscussionForum.Contracts;
using DiscussionForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRepositoryWrapper _repoWrapper;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        IRepositoryWrapper repoWrapper,
        ILogger<CategoriesController> logger
    )
    {
        _repoWrapper = repoWrapper;
        _logger = logger;
    }
}
