using HuynmHE176493.Business.IService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HuynmHE176493.Data.Models;

namespace HuynmHE176493.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly INewsArticleService _newsArticleService;
        public HomeController(ILogger<HomeController> logger, INewsArticleService newsArticleService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            var articles = _newsArticleService.GetAll()
                .Where(n => n.NewsStatus == true)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
