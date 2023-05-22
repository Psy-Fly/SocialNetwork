using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Models.ViewModels;

namespace SocialNetwork.Controllers;

public class AccountManagerController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IMapper mapper;

    public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = mapper.Map<User>(model);

            var result = await signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }

        return RedirectToAction("Index", "Home");
    }   
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}