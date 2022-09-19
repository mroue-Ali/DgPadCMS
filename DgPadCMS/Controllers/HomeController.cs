using Microsoft.AspNetCore.Mvc;

namespace DgPadCMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        
    }
}
