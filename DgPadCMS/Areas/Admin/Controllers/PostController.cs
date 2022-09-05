using DgPadCMS.Infrastructure;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DgPadCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DgPadCMSContext context;
        public PostController(DgPadCMSContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.posts.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.PostTypeId = new SelectList(context.postTypes.OrderBy(x => x.Title), "Id", "Title");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostTermViewModel postTermViewModel)
        {
            ViewBag.PostTypeId = new SelectList(context.postTypes.OrderBy(x => x.Title), "Id", "Title");


            if (ModelState.IsValid)
            {
                
                context.posts.Add(postTermViewModel.Post);
                await context.SaveChangesAsync();
                return RedirectToAction("CreateSecond", new {Id=postTermViewModel.Post.PostTypeId});
            }
            return View(postTermViewModel.Post);
        }
        public IActionResult CreateSecond(int Id)
        {
            ViewBag.PostType = context.postTypes.Find(Id).Title.ToString();
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSecond(PostTermViewModel postTermViewModel)
        {
            ViewBag.PostTypeId = new SelectList(context.postTypes.OrderBy(x => x.Title), "Id", "Title");


            if (ModelState.IsValid)
            {

                context.posts.Add(postTermViewModel.Post);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postTermViewModel.Post);
        }
    }
}
