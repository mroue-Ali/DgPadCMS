using DgPadCMS.Infrastructure;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicWebsite.Models;
using System.Collections.Generic;
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
        public async Task<IActionResult> PostByTerm(int id)
        {
            var postterms = await context.postTerms.Include(x => x.Post).Where(x => x.TermId == id).ToListAsync();
            var posts = new List<Post>();
            ViewBag.Term = await context.terms.FindAsync(id);
            foreach (var post in postterms)
            {
                posts.Add(post.Post);
            }
            return View(posts);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var pt = await context.postTerms.Include(x => x.Term).Where(x=>x.PostId==id).ToListAsync();
            var p=await context.posts.Include(x => x.postTerms).ThenInclude(x=>x.Term).ToListAsync();
        

            var post = await context.posts.FindAsync(id);
            DetailViewModel detailViewModel = new DetailViewModel();
            detailViewModel.post = post;
            detailViewModel.posts = p;
            detailViewModel.postTerms = pt;
            return View(detailViewModel);
        }
    }
}
