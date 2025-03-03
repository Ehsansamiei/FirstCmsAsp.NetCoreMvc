using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakeCms.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {

            var userRole = HttpContext.Session.GetString("UserRole");
            
            if(userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            return View("~/Areas/Admin/Views/Admin/Index.cshtml");
        }
    }
}
