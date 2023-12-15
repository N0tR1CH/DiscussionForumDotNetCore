using DiscussionForum.Contracts;
using DiscussionForum.DTOs;
using DiscussionForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiscussionForum.Controllers;

[ApiController]
[Route("/categories")]
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

    // GET /categories
    [HttpGet(Name = "GetAllCategories")]
    public async Task<IActionResult> Index()
    {
        // Get all categories
        var categories = _repoWrapper.Category.FindAll();

        // Return all categories
        var response = new RestDTO<IEnumerable<Category>>
        {
            Data = categories,
            Links = new List<LinkDTO>
            {
                new LinkDTO(Url.Action("Index", "Categories"), "self", "GET")
            }
        };

        return Ok(response);
    }

    // GET /categories/{id}
    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<IActionResult> Show(int id)
    {
        // Get category by id
        var category = _repoWrapper.Category.FindByCondition(c => c.Id == id).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        // Return category
        var response = new RestDTO<Category?>
        {
            Data = category,
            Links = new List<LinkDTO>
            {
                new LinkDTO(Url.Action("Show", "Categories", new { id }), "self", "GET")
            }
        };

        return Ok(response);
    }

    // POST /categories
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

    // PUT /categories/{id}
    [HttpPut("{id}", Name = "UpdateCategory")]
    public async Task<IActionResult> Update(CategoryDTO input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Get id from route
        var id = int.Parse(RouteData.Values["id"].ToString());

        // Get category by id
        var category = _repoWrapper.Category.FindByCondition(c => c.Id == id).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        // Check model state
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState
                .Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(errorMessages);
        }

        // Update category
        category.Name = input.Name;
        category.Description = input.Description ?? string.Empty;
        category.LastModifiedDate = DateTime.UtcNow;

        // Update category in database
        try
        {
            _repoWrapper.Category.Update(category);
            _repoWrapper.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return StatusCode(500, "An error occurred while saving the entity changes.");
        }

        // Return updated category
        var url = Url.Action("Show", "Categories", new { id = category.Id }, Request.Scheme);

        if (url == null)
        {
            return BadRequest("Could not generate URL.");
        }

        var response = new RestDTO<Category?>
        {
            Data = category,
            Links = new List<LinkDTO> { new LinkDTO(url, "self", "GET") }
        };

        return Created(url, response);
    }

    // DELETE /categories/{id}
    [HttpDelete("{id}", Name = "DeleteCategory")]
    public async Task<IActionResult> destroy()
    {
        // Id from route
        var id = int.Parse(RouteData.Values["id"].ToString());

        // Get category by id
        var category = _repoWrapper.Category.FindByCondition(c => c.Id == id).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        // Delete category from database
        try
        {
            _repoWrapper.Category.Delete(category);
            _repoWrapper.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            return StatusCode(500, "An error occurred while saving the entity changes.");
        }

        // Return no content
        return NoContent();
    }
}
