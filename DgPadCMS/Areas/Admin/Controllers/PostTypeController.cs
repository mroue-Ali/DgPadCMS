using DgPadCMS.Infrastructure;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var taxonomyList= await context.taxonomies.ToListAsync();
            PostTypeViewModel postTypeViewModel = new PostTypeViewModel();
            postTypeViewModel.availabletaxonomies = taxonomyList;
            return View(postTypeViewModel);
            
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostTypeViewModel postTypeViewModel, List<int> taxonomyIdList)
        {
            PostType postType = new PostType();
            postType.Title = postTypeViewModel.Title;
            postType.Code = postTypeViewModel.Code;
            if (ModelState.IsValid)
            {
                var c = await context.postTypes.FirstOrDefaultAsync(x => x.Code == postTypeViewModel.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the post type alredy exists");


                    return View(postTypeViewModel);

                }

                context.postTypes.Add(postType);
                await context.SaveChangesAsync();
                foreach (var taxonomy in taxonomyIdList)
                {
                    PostTypeTaxonomy postTypeTaxonomy = new PostTypeTaxonomy()
                    {
                        taxonomyId = taxonomy,
                        postTypeId= postType.Id,
                    };

                    context.Add(postTypeTaxonomy);
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(postTypeViewModel);
        }
       
        public IActionResult Delete(PostType postType)
        {
            context.postTypes.Remove(postType);
            context.SaveChanges();
            return RedirectToAction("index");

        }


    }
}
