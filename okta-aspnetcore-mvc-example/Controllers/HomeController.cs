using System.Diagnostics;
using System.Linq;
using ConfigCat.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using okta_aspnetcore_mvc_example.Models;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace okta_aspnetcore_mvc_example.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.User.Claims.Where(claim => claim.Type == "email").Select(claim => claim.Value).FirstOrDefault();
            var user = new User(userEmail) {Email = userEmail};
            var client = new ConfigCatClient("tNHYCC8Nm0OPXt2LxXT4zQ/k-5ZmLLd10isguXVF6PrTw");
            var twitterFeedVisible = client.GetValue("twitterFeedVisible", false, user);
            return View(twitterFeedVisible);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(HttpContext.User.Claims);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}