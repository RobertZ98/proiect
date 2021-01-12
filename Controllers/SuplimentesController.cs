using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zagrean_Robert_project.Data;
using Zagrean_Robert_project.Models;

namespace Zagrean_Robert_project.Controllers
{
    public class SuplimentesController : Controller
    {
        private readonly SuplimenteContext _context;

        public SuplimentesController(SuplimenteContext context)
        {
            _context = context;
        }

        // GET: Suplimentes
        public async Task<IActionResult> Index(
 string sortOrder,
 string currentFilter,
 string searchString,
 int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var sups = from b in _context.Suplimentes
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                sups = sups.Where(s => s.Tip.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    sups = sups.OrderByDescending(b => b.Tip);
                    break;
                case "Price":
                    sups = sups.OrderBy(b => b.Pret);
                    break;
                case "price_desc":
                    sups = sups.OrderByDescending(b => b.Pret);
                    break;
                default:
                    sups = sups.OrderBy(b => b.Tip);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Suplimente>.CreateAsync(sups.AsNoTracking(), pageNumber ??
           1, pageSize));
        }

        // GET: Suplimentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supliment = await _context.Suplimentes
            .Include(s => s.Comenzis)
            .ThenInclude(e => e.Client)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            var suplimente = await _context.Suplimentes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suplimente == null)
            {
                return NotFound();
            }

            return View(suplimente);
        }

        // GET: Suplimentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suplimentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Tip,Producator,Pret")] Suplimente suplimente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(suplimente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }

            return View(suplimente);
        }

        // GET: Suplimentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplimente = await _context.Suplimentes.FindAsync(id);
            if (suplimente == null)
            {
                return NotFound();
            }
            return View(suplimente);
        }

        // POST: Suplimentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var suplimentToUpdate = await _context.Suplimentes.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Suplimente>(
            suplimentToUpdate,
            "",
            s => s.Tip, s => s.Producator, s => s.Pret))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(suplimentToUpdate);
        
        }

        // GET: Suplimentes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplimente = await _context.Suplimentes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suplimente == null)
            {
                return NotFound();
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again";
            }
            return View(suplimente);
        }

        // POST: Suplimentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suplimente = await _context.Suplimentes.FindAsync(id);
            if(suplimente == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Suplimentes.Remove(suplimente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }

        private bool SuplimenteExists(int id)
        {
            return _context.Suplimentes.Any(e => e.ID == id);
        }
    }
}
