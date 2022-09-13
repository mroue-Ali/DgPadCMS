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
           
            //if (ModelState.IsValid)
            //{
                postTypeViewModel.Code = postTypeViewModel.Title.ToLower().Replace(" ", "_");
                PostType postType = new PostType();
                postType.Title = postTypeViewModel.Title;
                postType.Code = postTypeViewModel.Code;
                postType.ImgChecked = postTypeViewModel.ImgChecked;
            postType.MediaChecked=postTypeViewModel.MediaChecked;
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

            //}
            //return View(postTypeViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var taxonomyList = await context.taxonomies.ToListAsync();
            var postType = await context.postTypes.FirstOrDefaultAsync(x => x.Id == id);
            PostTypeViewModel postTypeViewModel = new PostTypeViewModel();
            postTypeViewModel.availabletaxonomies = taxonomyList;
            postTypeViewModel.Title = postType.Title;
            postTypeViewModel.Code = postType.Code;
            postTypeViewModel.ImgChecked = postType.ImgChecked;
            postTypeViewModel.MediaChecked = postType.MediaChecked;
            return View(postTypeViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostTypeViewModel postTypeViewModel, int id, List<int> taxonomyIdList)
        {
         
            if (ModelState.IsValid)
            {
                postTypeViewModel.Code = postTypeViewModel.Title.ToLower().Replace(" ", "_");
                var postType = await context.postTypes.FirstOrDefaultAsync(x => x.Id == id);
                postType.Title = postTypeViewModel.Title;
                postType.Code = postTypeViewModel.Code;
                postType.ImgChecked = postTypeViewModel.ImgChecked;
                postType.MediaChecked = postTypeViewModel.MediaChecked;
                var c = await context.postTypes.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Code == postTypeViewModel.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the post type alredy exists");


                    return View(postTypeViewModel);

                }

                context.postTypes.Update(postType);
                await context.SaveChangesAsync();
                var ttt = await context.postTypeTaxonomies.Where(x => x.postTypeId == id).ToListAsync();
                foreach (var item in ttt)
                {
                    context.postTypeTaxonomies.Remove(item);

                }

                await context.SaveChangesAsync();
                foreach (var taxonomy in taxonomyIdList)
                {
                    PostTypeTaxonomy postTypeTaxonomy = new PostTypeTaxonomy()
                    {
                        taxonomyId = taxonomy,
                        postTypeId = postType.Id,
                    };

                    context.Add(postTypeTaxonomy);
                }
                await context.SaveChangesAsync();
                    
                return RedirectToAction("Index");

            }
            return View(postTypeViewModel);
        }
        public IActionResult Details(int id)
        {
           
            var postType = context.postTypes.FirstOrDefault(x => x.Id == id);
            ViewBag.selectedTaxonomies = context.postTypeTaxonomies.Where(x => x.postTypeId == id).Include(x => x.Taxonomy).ToList();
           
            return View(postType);
        }

        public IActionResult Delete(PostType postType)
        {
            context.postTypes.Remove(postType);
            context.SaveChanges();
            return RedirectToAction("index");

        }


    }
}
