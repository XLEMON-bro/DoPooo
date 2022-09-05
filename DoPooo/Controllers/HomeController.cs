using DoPooo.Models;
using DoPooo.Services;
using DoPooo.Services.IServices;
using DoPooo.ThirdPartyApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Security.Claims;

namespace DoPooo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IConfiguration _configuration;

        private IMemoryCache _cache;

        private IYouTubeService _youTubeService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMemoryCache cache)
        {
            _logger = logger;
            _configuration = configuration;
            _cache = cache;
            _youTubeService = new YouTubeService(_cache, _configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            //var ur = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var viewModel = await _youTubeService.GetMostPopularByTopic();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}