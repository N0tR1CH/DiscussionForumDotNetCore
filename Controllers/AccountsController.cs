using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DiscussionForum.DTOs;
using DiscussionForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApiUser> _userManager;
    private readonly SignInManager<ApiUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountsController(
        AppDbContext context,
        UserManager<ApiUser> userManager,
        SignInManager<ApiUser> signInManager,
        IConfiguration configuration
    )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = new ApiUser();

            newUser.UserName = input.UserName;
            newUser.Email = input.Email;

            if (input.Password == null)
            {
                return BadRequest("Password required.");
            }

            var result = await _userManager.CreateAsync(newUser, input.Password);

            if (result.Succeeded)
            {
                return StatusCode(201, $"User {newUser.UserName} created.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (input.UserName == null)
            {
                return BadRequest("Username required.");
            }

            if (input.Password == null)
            {
                return BadRequest("Password required.");
            }

            var user = await _userManager.FindByNameAsync(input.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
            {
                return BadRequest("Invalid credentials.");
            }
            else
            {
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"])
                    ),
                    SecurityAlgorithms.HmacSha256
                );

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

                claims.AddRange(
                    (await _userManager.GetRolesAsync(user)).Select(
                        r => new Claim(ClaimTypes.Role, r)
                    )
                );

                var jwtObject = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(300),
                    signingCredentials: signingCredentials
                );

                // Jwt token as string
                var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

                return StatusCode(StatusCodes.Status200OK, jwtString);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
