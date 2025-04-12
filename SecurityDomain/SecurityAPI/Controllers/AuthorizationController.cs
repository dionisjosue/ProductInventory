using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityDomain.DTO;
using SecurityDomain.Models;
using SecurityDomain.Repositories;
using SharedLibrary.SharedItems.Shared;

namespace SecurityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{


    private readonly ILogger<AuthorizationController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IRepositoryWrapper _repo;


    public AuthorizationController(ILogger<AuthorizationController> logger,
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IRepositoryWrapper repo)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _repo = repo;
    }


    [HttpGet("token")]
    public async Task<ActionResult> Token(string username, string password)
    {
        var resp = new ResponseGeneric<UserAuthDTO>();

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            //await _loginAuditRepository.LoginAudit(loginAudit);
            resp.Result.Forbidden(new List<string>() { "Usuario o Contrasena incorrectos" });
            return StatusCode(resp.Result.Code, resp);
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {

            if (!user.IsActive)
            {
                resp.Result.Forbidden(new List<string>() { "Usuario o Contrasena incorrectos" });
                return StatusCode(resp.Result.Code, resp);
            }

            var authUser = await _repo.AppUser.BuildUserAuthObject(user);
            resp.Data = authUser;
        }

        return StatusCode(resp.Result.Code, resp);

    }

}

