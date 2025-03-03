using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer;
using DrakeCms.Areas.Admin.Dtos;
using Page = DataLayer.Page;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace DrakeCms.Areas.Admin.Controllers
{ 

    [Route("Admin/Pages")]
    [Authorize]

    public class PagesController : Controller
    {
        private readonly IPageGroupRepository _pageGroupRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PagesController(IPageGroupRepository pageGroupRepository, IPageRepository pageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _pageGroupRepository = pageGroupRepository;
            _pageRepository = pageRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Admin/Pages
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            return View("~/Areas/Admin/Views/Pages/Index.cshtml", _pageRepository.GetAllPage());
        }


        // GET: Admin/Pages/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            var page = _pageRepository.GetPage(id);
            if (page == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/Pages/Details.cshtml", page);
        }


        // GET: Admin/Pages/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            ViewBag.GroupId = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupId", "GroupTitile");
            return View("~/Areas/Admin/Views/Pages/Create.cshtml");
        }


        // POST: Admin/Pages/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PageDto pageDto)
        {
            if (ModelState.IsValid)
            {
                var page = new Page
                {
                    GroupId = pageDto.GroupId,
                    Title = pageDto.Title,
                    ShortDescription = pageDto.ShortDescription,
                    Text = pageDto.Text,
                    ShowInSlider = pageDto.ShowInSlider,
                    CreateDate = DateTime.Now,
                    Visit = 0
                };

                if (pageDto.ImageFile != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(pageDto.ImageFile.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "PageImages");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string filePath2 = Path.Combine(filePath, fileName);
                    using (var fileStream = new FileStream(filePath2, FileMode.Create))
                    {
                        await pageDto.ImageFile.CopyToAsync(fileStream);
                    }
                    page.ImageName = fileName;
                }
                page.pageGroup = _pageGroupRepository.GetPageGroupById(pageDto.GroupId);
                _pageRepository.InsertPage(page);
                _pageRepository.SaveAsync();
                return View("~/Areas/Admin/Views/Pages/Index.cshtml", _pageRepository.GetAllPage());
            }
            return View("~/Areas/Admin/Views/Pages/Create.cshtml", pageDto);
        }


        // GET: Admin/Pages/Edit
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "Admin")
            {
                return RedirectToAction("Error404", "Home");
            }

            var page = _pageRepository.GetPage(id);
            if (page == null)
            {
                return NotFound();
            }

            ViewBag.GroupId = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupId", "GroupTitile", page.GroupId);
            var pageDto = new PageDto
            {
                PageId = page.PageId,
                GroupId = page.GroupId,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Text = page.Text,
                ShowInSlider = page.ShowInSlider,
                ImageFile = null
            };
            return View("~/Areas/Admin/Views/Pages/Edit.cshtml", pageDto);

        }


        // POST: Admin/Pages/Edit
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, PageDto pageDto)
        {


            var page = _pageRepository.GetPage(id);
            if (page == null)
            {
                return NotFound();
            }

            page.GroupId = pageDto.GroupId;
            page.Title = pageDto.Title;
            page.ShortDescription = pageDto.ShortDescription;
            page.Text = pageDto.Text;
            page.ShowInSlider = pageDto.ShowInSlider;

            if (pageDto.ImageFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(pageDto.ImageFile.FileName);
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "pageImages");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string filePath2 = Path.Combine(filePath, fileName);
                using (var fileStrem = new FileStream(filePath2, FileMode.Create))
                {
                    await pageDto.ImageFile.CopyToAsync(fileStrem);
                    page.ImageName = fileName;
                }
                page.pageGroup = _pageGroupRepository.GetPageGroupById(pageDto.GroupId);
                page.GroupId = pageDto.GroupId;

                page.Title = pageDto.Title;
                page.ShortDescription = pageDto.ShortDescription;
                page.Text = pageDto.Text;

                page.ShowInSlider = pageDto.ShowInSlider;
                //_pageRepository.SaveAsync();

                return View("~/Areas/Admin/Views/Pages/Index.cshtml", _pageRepository.GetAllPage());
            }
            _pageRepository.SaveAsync();
            return View("~/Areas/Admin/Views/Pages/Index.cshtml", _pageRepository.GetAllPage());
        }


        // GET: Admin/Pages/Delete
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
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
                var page = _pageRepository.GetPage(id.Value);
                if (page == null)
                {
                    return NotFound();
                }
                else
                {
                    return View("~/Areas/Admin/Views/Pages/Delete.cshtml", page);
                }
            }
        }


        // POST: Admin/Pages/Delete
        [HttpPost("Delete/{id}")]
        public IActionResult Delete(Page page)
        {
            if (page.PageId == null)
            {
                return NotFound();
            }

            _pageRepository.DeletePage(page);
            _pageRepository.SaveAsync();
            return View("~/Areas/Admin/Views/Pages/Index.cshtml", _pageRepository.GetAllPage());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pageGroupRepository.Dispose();
                _pageRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}