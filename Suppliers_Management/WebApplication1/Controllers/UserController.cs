using WebApplication1.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.AuthDTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        
        private readonly UserAuthenticationService _authService;

        public UserController(UserAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.Authenticate(userLoginRequest.Username, userLoginRequest.Password);
                if (user != null)
                {
                    // Set the user as authenticated
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username!)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(userLoginRequest);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

    }
}
