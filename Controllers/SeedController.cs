using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]/[action]")]
[Authorize(Roles = RoleNames.Administrator)]
public class SeedController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApiUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedController(
        AppDbContext context,
        UserManager<ApiUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> AuthData()
    {
        int rolesCreated = 0;
        int usersAddedToRoles = 0;

        if (!await _roleManager.RoleExistsAsync(RoleNames.Moderator))
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleNames.Moderator));
            rolesCreated++;
        }

        if (!await _roleManager.RoleExistsAsync(RoleNames.Administrator))
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleNames.Administrator));
            rolesCreated++;
        }

        var testModerator = await _userManager.FindByNameAsync("TestModerator");

        if (
            testModerator != null
            && !await _userManager.IsInRoleAsync(testModerator, RoleNames.Moderator)
        )
        {
            await _userManager.AddToRoleAsync(testModerator, RoleNames.Moderator);
            usersAddedToRoles++;
        }

        var testAdministrator = await _userManager.FindByNameAsync("TestAdministrator");

        if (
            testAdministrator != null
            && !await _userManager.IsInRoleAsync(testAdministrator, RoleNames.Administrator)
        )
        {
            await _userManager.AddToRoleAsync(testAdministrator, RoleNames.Administrator);
            await _userManager.AddToRoleAsync(testAdministrator, RoleNames.Moderator);
            usersAddedToRoles++;
        }

        return new JsonResult(
            new { rolesCreated = rolesCreated, usersAddedToRoles = usersAddedToRoles }
        );
    }
}
