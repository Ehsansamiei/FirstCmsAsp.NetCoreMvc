using DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;

namespace DrakeCms.Controllers
{
    public class AccountController : Controller
    {
        private readonly DrakeCmsContext _db;
        public AccountController(DrakeCmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _db.AdminLogin.FirstOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);
                
                if(user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("UserId", user.LoginId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    HttpContext.Session.SetString("UserRole", user.Role);
                    return Redirect("/");
                }
                ModelState.AddModelError("", "Invalid Username or password");

            }
            //return View(login);
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}