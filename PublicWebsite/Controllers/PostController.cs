using Microsoft.AspNetCore.Mvc;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
