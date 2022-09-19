using DgPadCMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicWebsite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class IndexController : Controller
    {
        private readonly DgPadCMSContext context;
        public IndexController(DgPadCMSContext context)
        {
            this.context = context;

        }


        public IActionResult Index()
        {

            //var todaysPosts = context.posts.Include(x=>x.postType).Where(x => x.CreationDate.Day == DateTime.Today.Day).ToList();
            var recentPosts = context.posts.Include(x=>x.postType).OrderByDescending(x=>x.CreationDate).ToList();



            return View( recentPosts);
        }



            

        


    }
}
