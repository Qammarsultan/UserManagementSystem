using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Interface;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRegisterRepo _registerRepo;
        public RegisterController(IUserRegisterRepo registerRepo)
        {
            _registerRepo = registerRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Signup(UserRegister user)
        {
           var result = await _registerRepo.Signup(user); 
            return RedirectToAction("Signin");
        }
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Signin(UserRegister user)
        {
            var token = await _registerRepo.Signin(user);
            if(token == null)
            {
                return View("Signin");
            }

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.Now.AddDays(1)

            });
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return RedirectToAction("Signin", "Register");
        }




        [Authorize]
        public IActionResult GetAuthResult()
        {
            return RedirectToAction("GetUsersList", "Account");
        }
    }
}
