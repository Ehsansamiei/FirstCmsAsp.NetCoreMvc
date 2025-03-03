using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakeCms.Areas.Admin.Controllers
{
    [Route("Admin/PageGroup")]
    [Authorize]
    public class PageGroupController : Controller
    {

        private readonly IPageGroupRepository _pageGroupRepository;


        public PageGroupController(IPageGroupRepository pageGroupRepository)
        {
            _pageGroupRepository = pageGroupRepository;
        }


        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            var GroupPages = _pageGroupRepository.GetAllGroups();
            return View("~/Areas/Admin/Views/PageGroup/Index.cshtml", GroupPages);
        }


        [HttpGet("Detail/{id}")]
        public IActionResult Detail(int id)
        {

            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            var SelectedGroupPage = _pageGroupRepository.GetPageGroupById(id);
            return View("~/Areas/Admin/Views/PageGroup/Detail.cshtml", SelectedGroupPage);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {

            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            return View("~/Areas/Admin/Views/PageGroup/Create.cshtml");
        }


        [HttpPost("Create")]
        public IActionResult Create(PageGroup pageGroup)
        {

            _pageGroupRepository.InsertGroup(pageGroup);
            _pageGroupRepository.SaveAsync();

            var GroupPages = _pageGroupRepository.GetAllGroups();
            return View("~/Areas/Admin/Views/PageGroup/Index.cshtml", GroupPages);
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var GroupPage = _pageGroupRepository.GetPageGroupById(id.Value);
            if (GroupPage == null)
            {
                return NotFound();
            }
            return View("~/Areas/Admin/Views/PageGroup/Edit.cshtml", GroupPage);
        }


        [HttpPost("Edit")]
        public IActionResult Edit(int id, PageGroup pageGroup)
        {

            var GroupPage = _pageGroupRepository.GetPageGroupById(id);

            if (GroupPage == null)
            {
                return NotFound();
            }
            else
            {
                GroupPage.GroupTitile = pageGroup.GroupTitile;
            }

            _pageGroupRepository.SaveAsync();
            var GroupPages = _pageGroupRepository.GetAllGroups();
            return View("~/Areas/Admin/Views/PageGroup/Index.cshtml", GroupPages);
        }


        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var GroupPageItem = _pageGroupRepository.GetPageGroupById(id.Value);
                if (GroupPageItem == null)
                {
                    return NotFound();
                }
                return View("~/Areas/Admin/Views/PageGroup/Delete.cshtml", GroupPageItem);
            }
        }


        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PageGroup? p = _pageGroupRepository.GetPageGroupById(id);
            if (p != null)
            {
                _pageGroupRepository.DeleteGroup(p);
                await _pageGroupRepository.SaveAsync();
                var GroupPages = _pageGroupRepository.GetAllGroups();
                return View("~/Areas/Admin/Views/PageGroup/Index.cshtml", GroupPages);
            }
            else
            {
                return NotFound();
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pageGroupRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}