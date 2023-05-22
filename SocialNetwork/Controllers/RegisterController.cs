using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Models.ViewModels;

namespace SocialNetwork.Controllers;

public class RegisterController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IMapper mapper;

    public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.mapper = mapper;
    }

    [Route("Register")]
    [HttpGet]
    public IActionResult Register()
    {
        return View("Register");
    }

    [Route("RegisterPart2")]
    [HttpGet]
    public IActionResult RegisterPart2(RegisterViewModel model)
    {
        return View("RegisterPart2", model);
    }



    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = mapper.Map<User>(model);
               
            var result = await userManager.CreateAsync(user, model.PasswordReg);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View("RegisterPart2", model);
    }

}