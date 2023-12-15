using DiscussionForum.Contracts;
using DiscussionForum.DTOs;
using DiscussionForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    [HttpPost(Name = "CreateCategory")]
    public async Task<IActionResult> Create(CategoryDTO input)
    {
        // Check model state
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState
                .Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(errorMessages);
        }

        if (input.Name == null)
        {
            return BadRequest("Name required.");
        }

        // Create new category
        var newCategory = new Category
        {
            Name = input.Name,
            Description = input.Description ?? string.Empty,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow
        };

        // Add new category to database
        try
        {
            await _repoWrapper.Category.CreateAsync(newCategory);
            _repoWrapper.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return StatusCode(500, "An error occurred while saving the entity changes.");
        }

        // Return new category
        var url = Url.Action("Get", "Categories", new { id = newCategory.Id }, Request.Scheme);

        if (url == null)
        {
            return BadRequest("Could not generate URL.");
        }

        var response = new RestDTO<Category?>
        {
            Data = newCategory,
            Links = new List<LinkDTO> { new LinkDTO(url, "self", "GET") }
        };

        return Created(url, response);
    }
}
