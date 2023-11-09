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

        // Service for user authentication
        private readonly UserAuthenticationService _authService;

        // Constructor that injects the authentication service
        public UserController(UserAuthenticationService authService)
        {
            _authService = authService;
        }

        // Displays the login page
        [HttpGet]
        [Route("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        // Processes the login request
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user
                var user = _authService.Authenticate(userLoginRequest.Username, userLoginRequest.Password);

                if (user != null)
                {
                    // If user is valid, sign in and redirect to home
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username!)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                // If user is not valid, show login error
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            // Return to login view with validation errors
            return View(userLoginRequest);
        }

        // Logs the user out
        [Route("[action]")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Sign out and redirect to the login page
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

    }
}
