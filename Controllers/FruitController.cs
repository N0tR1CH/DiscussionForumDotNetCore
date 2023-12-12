using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FruitsController : ControllerBase
{
    List<string> _fruits = ["Apple", "Banana", "Orange"];

    [HttpGet("fruits")]
    public IEnumerable<string> GetAllFruits()
    {
        return _fruits;
    }
}
