using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_application_task.Data;
using web_application_task.Dtos.User;
using web_application_task.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_application_task.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthRepository _authRepo;

        public UserController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Check if the user is already authenticated
            if (User.Identity!.IsAuthenticated)
            {
                // Redirect to the profile page
                return RedirectToAction("Profile", "User");
            }

            var model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }
            // Validate the user credentials using the Login service
            var response = await _authRepo.Login(userLoginDto);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Message);
                return View(userLoginDto);
            }

            var authenticatedUser = response.Data;


            // Store the token in the cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser!.Id.ToString()), // Add the user ID as the value
                new Claim(ClaimTypes.Email, userLoginDto.Email)
             };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false // Set to true if you want to persist the cookie across sessions
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirect to the desired page
            return RedirectToAction("Profile");
        }


        [HttpGet]
        public IActionResult Register()
        {
            // Check if the user is already authenticated
            if (User.Identity!.IsAuthenticated)
            {
                // Redirect to the profile page
                return RedirectToAction("Profile", "User");
            }

            var model = new UserRegisterDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDto);
            }

            var user = new Models.User
            {
                Email = userRegisterDto.Email
            };

            var response = await _authRepo.Register(userRegisterDto);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Message);
                return View(userRegisterDto);
            }

            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            // Clear the authentication cookie
            Response.Cookies.Delete("token");

            return RedirectToAction("Login", "User"); // Redirect to the login page or any other desired page
        }
    }
}

