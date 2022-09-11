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
    public class TermController : Controller
    {
        private readonly DgPadCMSContext context;
        public TermController(DgPadCMSContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.terms.OrderBy(x => x.Id).Include(x=>x.taxonomy).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.taxonomies.OrderBy(x => x.Name), "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.taxonomies.OrderBy(x => x.Name), "Id", "Name");

            if (ModelState.IsValid)
            {
                term.Code = term.Name.ToLower().Replace(" ", "_");
                var c = await context.terms.FirstOrDefaultAsync(x => x.Code == term.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the term alredy exists");


                    return View();

                }
                context.terms.Add(term);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(term);
        }
        public IActionResult Delete(Term term)
        {
            context.terms.Remove(term);
            context.SaveChanges();
            return RedirectToAction("index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TaxonomyId = new SelectList(context.taxonomies.OrderBy(x => x.Name), "Id", "Name");
            var term = await context.terms.FindAsync(id);
            return View(term);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.taxonomies.OrderBy(x => x.Name), "Id", "Name");

            if (ModelState.IsValid)
            {
                term.Code = term.Name.ToLower().Replace(" ", "_");

                var c = await context.terms.Where(x=>x.Id!=term.Id).FirstOrDefaultAsync(x => x.Code == term.Code);
                if (c != null)
                {
                    ModelState.AddModelError("", "the term alredy exists");


                    return View();

                }
                context.terms.Update(term);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(term);
        }
    }
}
