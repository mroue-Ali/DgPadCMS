using DgPadCMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly DgPadCMSContext context;

        public PostController(DgPadCMSContext context)
        {
            this.context = context;

        }
        public async Task<IActionResult> Index(int id)
        {
            var posts = await context.posts.Include(x=>x.postType).Where(x => x.PostTypeId == id).ToListAsync();
            var postType = await context.postTypes.FindAsync(id);
            ViewBag.PostType = postType.Title;
            return View(posts);
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.terms = await context.posts.Include(x => x.postTerms).ThenInclude(x => x.Term).ToListAsync();
           
            var post = await context.posts.FindAsync(id);
            return View(post);
        }
    }
}
