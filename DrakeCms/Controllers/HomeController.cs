using System.Diagnostics;
using DataLayer;
using DrakeCms.Attributes;
using DrakeCms.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrakeCms.Controllers;

[MyController("HomeController")]
public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    IPageGroupRepository _pageGroupRepository;
    IPageRepository _pageRepository;
    public HomeController(ILogger<HomeController> logger, IPageGroupRepository pageGroupRepository, IPageRepository pageRepository)
    {
        _logger = logger;
        _pageGroupRepository = pageGroupRepository;
        _pageRepository = pageRepository;
    }

    public IActionResult Index() {
        ViewData["ShowGroups"] = _pageGroupRepository.GetGroupForView();
        ViewData["ShowInSlider"] = _pageRepository.PagesInSlider();
        return View();
    }

    public IActionResult Slider()
    {
        ViewData["ShowInSlider"] = _pageRepository.PagesInSlider();
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult Error404()
    {
        return View();
    }
    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
