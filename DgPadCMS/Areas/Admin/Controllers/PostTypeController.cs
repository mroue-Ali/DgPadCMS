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
    public class PostTypeController : Controller
    {
        private readonly DgPadCMSContext context;
        public PostTypeController(DgPadCMSContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.postTypes.OrderBy(x => x.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var taxonomyList= await context.taxonomies.OrderBy(x=>x.Id).ToListAsync();
            PostTypeViewModel postTypeViewModel = new PostTypeViewModel();
            postTypeViewModel.taxonomies=taxonomyList;
            
           return View(postTypeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostTypeViewModel postTypeViewModel)
        {

            if (ModelState.IsValid)
            {
                var c = await context.postTypes.FirstOrDefaultAsync(x => x.Code == postTypeViewModel.PostType.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the post type alredy exists");


                    return View();

                }
                var taxonomyList = await context.taxonomies.OrderBy(x => x.Id).ToListAsync();
                postTypeViewModel.taxonomies = taxonomyList;
                foreach (var item in postTypeViewModel.taxonomies)
                {
                    if (item.isCheked == true)
                    {
                        PostTypeTaxonomy postTypeTaxonomy = new PostTypeTaxonomy();
                        postTypeTaxonomy.postTypeId = postTypeViewModel.PostType.Id;
                        postTypeTaxonomy.taxonomyId=item.Id;
                        context.postTypeTaxonomies.Add(postTypeTaxonomy);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                    }
                }
                context.postTypes.Add(postTypeViewModel.PostType);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postTypeViewModel.PostType);
        }
        public IActionResult Delete(PostType postType)
        {
            context.postTypes.Remove(postType);
            context.SaveChanges();
            return RedirectToAction("index");

        }


    }
}
