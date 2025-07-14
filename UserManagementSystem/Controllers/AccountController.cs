using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Interface;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserInfo _userInfo;
        public AccountController(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserInfo user)
        {
            var response = await _userInfo.CreateUser(user);
            if (response == null)
                return View(user);
            return RedirectToAction("GetUsersList");
        }


        [HttpGet("EditUser/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(int Id)
        {
            var response = await _userInfo.GetUserById(Id);
            if (response == null)
                return RedirectToAction("GetUsersList");
            return View(response);
        }

        [HttpPost("EditUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(UserInfo user)
        {
            var response = await _userInfo.EditUser(user);
            if (response <= 0)
                return View(response);
            return RedirectToAction("GetUsersList");

        }

        [HttpGet]
        public async Task<IActionResult> GetUsersList()
        {
            var response = await _userInfo.GetUsersList();
            if (response == null)
                return BadRequest("No users are found here");
            return View(response);

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var response = await _userInfo.DeleteUser(Id);
            if (response <= 0)
                return RedirectToAction("ErroPage");
            return RedirectToAction("GetUsersList");

        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
