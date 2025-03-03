using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace DrakeCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private  DrakeCmsContext _db;
        private IPageGroupRepository _pageGroupRepository;
        private IPageRepository _pageRepository;

        public NewsController(DrakeCmsContext db, IPageGroupRepository pageGroupRepository, IPageRepository pageRepository)
        {
            _db = db;
            _pageGroupRepository = pageGroupRepository;
            _pageRepository = pageRepository;
        }

        [HttpGet("ShowGroups")]
        public IActionResult ShowGroups()
        {
            var GroupForView = _pageGroupRepository.GetGroupForView();
            if(GroupForView == null)
            {
                return NotFound();
            }
            return PartialView("~/Areas/Admin/Views/News/ShowGroups.cshtml",GroupForView);
        }

        [Route("/Pages/{id}")]
        public IActionResult DetailsOfPage(int id)
        {
            var page = _pageRepository.GetPage(id);
            if(page == null)
            {
                return NotFound();
            }
            return View("~/Areas/Admin/Views/News/DetailsOfPage.cshtml", page);
        }
    }
}