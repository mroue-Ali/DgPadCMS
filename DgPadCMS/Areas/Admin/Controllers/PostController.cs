using DgPadCMS.Infrastructure;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DgPadCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DgPadCMSContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PostController(DgPadCMSContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;

        }
        
        public async Task<IActionResult> Index()
        {
            return View(await context.posts.Include(x => x.postType).ToListAsync());
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
                postTermViewModel.Post.CreationDate = DateTime.Now;
                context.posts.Add(postTermViewModel.Post);
                await context.SaveChangesAsync();
                int postId = postTermViewModel.Post.Id;
                return RedirectToAction("CreateSecond", new { id = postTermViewModel.Post.PostTypeId, postID = postId });
            }
            return View(postTermViewModel.Post);
        }
        public IActionResult CreateSecond(int id, int postID)
        {
            ViewBag.PostType = context.postTypes.Find(id);
            
            PostTermViewModel postTermViewModel = new PostTermViewModel();
            postTermViewModel.postTypeTaxonomies = context.postTypeTaxonomies.Where(x => x.postTypeId == id).Include(x => x.Taxonomy).ThenInclude(x => x.terms).ToList();
            postTermViewModel.Post = context.posts.Find(postID);
            return View(postTermViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSecond(PostTermViewModel postTermViewModel, List<int> termIdList)
        {
            int id = postTermViewModel.Post.Id;
            var post = await context.posts.FindAsync(id);
            post.Detail = postTermViewModel.Post.Detail;
            post.Summary = postTermViewModel.Post.Summary;

            //if (ModelState.IsValid)
            //{
               
                string imageName = "nooimage.png";
                if (postTermViewModel.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Photos");
                    imageName = Guid.NewGuid().ToString() + "_" + postTermViewModel.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await postTermViewModel.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                string videoName = "novideo.mp4";
                if (postTermViewModel.MediaUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Videos");
                    videoName = Guid.NewGuid().ToString() + "_" + postTermViewModel.MediaUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, videoName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await postTermViewModel.MediaUpload.CopyToAsync(fs);
                    fs.Close();
                }
                post.Media = videoName;
                post.Image = imageName;
                context.posts.Update(post);
                await context.SaveChangesAsync();

                foreach (var termId in termIdList)
                {
                    PostTerm postTerm = new PostTerm()
                    {


                        TermId = termId,
                        PostId = postTermViewModel.Post.Id,
                    };

                    context.postTerms.Add(postTerm);
                }
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            //}
            //return View(postTermViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            PostTermViewModel postTermViewModel = new PostTermViewModel();
            postTermViewModel.Post = context.posts.Find(id);
            int postTypeId=postTermViewModel.Post.PostTypeId;
            postTermViewModel.postTypeTaxonomies = await context.postTypeTaxonomies.Where(x => x.postTypeId == postTypeId).Include(x => x.Taxonomy).ThenInclude(x => x.terms).ToListAsync();

            ViewBag.PostType = context.postTypes.Find(postTypeId);
          
            return View(postTermViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostTermViewModel postTermViewModel, List<int> termIdList)
        {
            int id = postTermViewModel.Post.Id;
            var post = await context.posts.FindAsync(id);
            post.Detail = postTermViewModel.Post.Detail;
            post.Title = postTermViewModel.Post.Title;
            post.Summary = postTermViewModel.Post.Summary;
            //if (ModelState.IsValid)
            //{

            if (postTermViewModel.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Photos");

                if (!string.Equals(post.Image, "nooimage.png"))
                {
                    string oldImagePath = Path.Combine(uploadsDir, post.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string imageName = Guid.NewGuid().ToString() + "_" + postTermViewModel.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await postTermViewModel.ImageUpload.CopyToAsync(fs);
                fs.Close();
                post.Image = imageName;
            }



           if (postTermViewModel.MediaUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Videos");

                if (!string.Equals(post.Media, "novideo.mp4"))
                {
                    string oldMediaPath = Path.Combine(uploadsDir, post.Media);
                    if (System.IO.File.Exists(oldMediaPath))
                    {
                        System.IO.File.Delete(oldMediaPath);
                    }
                }

                string MediaName = Guid.NewGuid().ToString() + "_" + postTermViewModel.MediaUpload.FileName;
                string filePath = Path.Combine(uploadsDir, MediaName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await postTermViewModel.MediaUpload.CopyToAsync(fs);
                fs.Close();
                post.Media = MediaName;
            }




            context.posts.Update(post);
                await context.SaveChangesAsync();
                var pt = context.postTerms.Where(x => x.PostId == post.Id).ToList();
                foreach (var item in pt)
                {
                    context.postTerms.Remove(item);
                }
               await context.SaveChangesAsync();
                foreach (var termId in termIdList)
                {
                    PostTerm postTerm = new PostTerm()
                    {
                        TermId = termId,
                        PostId = postTermViewModel.Post.Id,
                    };

                    context.postTerms.Add(postTerm);
                }
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            //}
            //return View(postTermViewModel.Post);
        }
      
        public IActionResult Details(int id)
        {

            var post = context.posts.Find(id);
            ViewBag.Terms = context.postTerms.Where(x => x.PostId == id).Include(x => x.Term).ToList();

            return View(post);
        }
        public IActionResult Delete(int id)
        {
            var post = context.posts.Find(id);
            var pt=context.postTerms.Where(x => x.PostId == post.Id).ToList();
            foreach(var item in pt)
            {
                context.postTerms.Remove(item);
            }
            context.SaveChanges();
            if (!string.Equals(post.Image, "nooimage.png"))
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/Photos");
                string oldImagePath = Path.Combine(uploadsDir, post.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            if (!string.Equals(post.Media, "novideo.mp4"))
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/Videos");
                string oldMediaPath = Path.Combine(uploadsDir, post.Media);
                if (System.IO.File.Exists(oldMediaPath))
                {
                    System.IO.File.Delete(oldMediaPath);
                }
            }
            context.posts.Remove(post);
            context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
