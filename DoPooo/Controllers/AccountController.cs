using DoPooo.Mapper;
using DoPooo.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DB.Repository;
using DB.Interfaces;
using DB.Entities;
using DB;
using DoPooo.Encyption;

namespace DoPooo.Controllers
{
    public class AccountController : Controller
    {
        private ICustomTempMapper _CustomTempMapper;
        private IGenericRepository<User> _db;

        public AccountController(MyContext context)
        {
            _CustomTempMapper = new CustomTempMapper();
            _db = new GenericRepository<User>(context);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var users = _db.FindFirstorDefault(u=>u.Password == userRegistrationViewModel.Password && 
                                                         u.Email == userRegistrationViewModel.Email);
                if(users == null)
                {
                    var user = _CustomTempMapper.MapToUser(userRegistrationViewModel);

                    await _db.AddAsync(user);

                    await Authentication(user.Email,user.Name);

                    return RedirectToAction("Index","Home");
                }
            }
            else
                ModelState.AddModelError("", "User with that email already exist");

            return View(userRegistrationViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            var userPassword = SHA256Encryption.EncryptText(userLoginViewModel.Password);

            if (ModelState.IsValid && !string.IsNullOrEmpty(userPassword))
            {
                var user = _db.FindFirstorDefault(u => u.Password == userPassword &&
                                                       u.Email == userLoginViewModel.Email);

                if (user != null)
                {
                    await Authentication(user.Email,user.Name);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Incorrect login or password");
            }

            return View(userLoginViewModel);
        }

        private async Task Authentication(string email, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            return RedirectToAction("Index", "Home");
        }
    }
}
