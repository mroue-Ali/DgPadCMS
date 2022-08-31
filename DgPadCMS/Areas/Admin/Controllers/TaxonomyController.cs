using DgPadCMS.Infrastructure;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DgPadCMS.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TaxonomyController : Controller
    {
        private readonly DgPadCMSContext context;
        public TaxonomyController(DgPadCMSContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.taxonomies.OrderBy(x=>x.Id).ToListAsync());
        }
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                var c = await context.taxonomies.FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the taxonomy alredy exists");


                    return View(taxonomy);

                }
                context.taxonomies.Add(taxonomy);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taxonomy);
        }
        public  IActionResult Delete(Taxonomy taxonomy)
        {
            context.taxonomies.Remove(taxonomy);
            context.SaveChanges();
            return RedirectToAction("index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            var taxonomy = await context.taxonomies.FindAsync(id);
            return View(taxonomy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                var c = await context.taxonomies.Where(x => x.Id != taxonomy.Id).FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the taxonomy already exists");


                    return View(taxonomy);

                }
                context.taxonomies.Update(taxonomy);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taxonomy);

        }


    }

    
}
