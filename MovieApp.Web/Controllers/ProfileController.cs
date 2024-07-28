using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
