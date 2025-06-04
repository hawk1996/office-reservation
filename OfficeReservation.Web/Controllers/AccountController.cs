using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OfficeReservation.Web.Models.Account;
using OfficeReservation.Services.DTOs.Authentication;

namespace OfficeReservation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly Services.Interfaces.IAuthenticationService authenticationService;
        public AccountController(Services.Interfaces.IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await authenticationService.LoginAsync(new LoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                });

                if (response.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, response.UserId.ToString()),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Name, response.Name)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties());

                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return LocalRedirect(model.ReturnUrl);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.ErrorMessage = response.ErrorMessage ?? "Invalid username or password.";
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
