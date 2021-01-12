using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zagrean_Robert_project.Data;
using Zagrean_Robert_project.Models;
using Zagrean_Robert_project.Models.LibraryViewModels;

namespace Zagrean_Robert_project.Controllers
{
    public class PublishersController : Controller
    {
        private readonly SuplimenteContext _context;

        public PublishersController(SuplimenteContext context)
        {
            _context = context;
        }

        // GET: Publishers
       
        public async Task<IActionResult> Index(int? id, int? SuplimenteID)
            {
                var viewModel = new PublisherIndexData();
                viewModel.Publishers = await _context.Publishers
                .Include(i => i.PublishedSuplements)
                .ThenInclude(i => i.Suplimente)
                .ThenInclude(i => i.Comenzis)
                .ThenInclude(i => i.Client)
                .AsNoTracking()
                .OrderBy(i => i.PublisherName)
                .ToListAsync();
                if (id != null)
                {
                    ViewData["PublisherID"] = id.Value;
                    Publisher publisher = viewModel.Publishers.Where(
                    i => i.ID == id.Value).Single();
                    viewModel.Suplemets = publisher.PublishedSuplements.Select(s => s.Suplimente);
                }
                if (SuplimenteID != null)
                {
                    ViewData["SuplimenteID"] = SuplimenteID.Value;
                    viewModel.Comenzi = viewModel.Suplemets.Where(x => x.ID == SuplimenteID).Single().Comenzis;
                }
                return View(viewModel);
         }
        

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PublisherName,Adresa")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.Include(i => i.PublishedSuplements).ThenInclude(i => i.Suplimente).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }
            PopulatePublishedSuplimenteData(publisher);
            return View(publisher);
        }
        private void PopulatePublishedSuplimenteData(Publisher publisher)
        {
            var allBooks = _context.Suplimentes;
            var publisherBooks = new HashSet<int>(publisher.PublishedSuplements.Select(c => c.PublisherID));
            var viewModel = new List<PublishedSuplimenteData>();
            foreach (var book in allBooks)
            {
                viewModel.Add(new PublishedSuplimenteData
                {
                    SuplimenteID = book.ID,
                    Tip = book.Tip,
                    IsPublished = publisherBooks.Contains(book.ID)
                });
            }
            ViewData["Suplimente"] = viewModel;
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedSuplements)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherToUpdate = await _context.Publishers.Include(i => i.PublishedSuplements).ThenInclude(i => i.Suplimente).FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Publisher>(publisherToUpdate,"",i => i.PublisherName, i => i.Adresa))
            {
                UpdatePublishedBooks(selectedSuplements, publisherToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedBooks(selectedSuplements, publisherToUpdate);
            PopulatePublishedSuplimenteData(publisherToUpdate);
            return View(publisherToUpdate);
        }
        private void UpdatePublishedBooks(string[] selectedBooks, Publisher publisherToUpdate)
        {
            if (selectedBooks == null)
            {
                publisherToUpdate.PublishedSuplements = new List<PublishedSuplements>();
                return;
            }
            var selectedBooksHS = new HashSet<string>(selectedBooks);
            var publishedBooks = new HashSet<int>
            (publisherToUpdate.PublishedSuplements.Select(c => c.Suplimente.ID));
            foreach (var book in _context.Suplimentes)
            {
                if (selectedBooksHS.Contains(book.ID.ToString()))
                {
                    if (!publishedBooks.Contains(book.ID))
                    {
                        publisherToUpdate.PublishedSuplements.Add(new PublishedSuplements
                        {
                            PublisherID = publisherToUpdate.ID, SuplementeID = book.ID
                        });
                    }
                }
                else
                {
                    if (publishedBooks.Contains(book.ID))
                    {
                        PublishedSuplements bookToRemove = publisherToUpdate.PublishedSuplements.FirstOrDefault(i
                       => i.SuplementeID == book.ID);
                        _context.Remove(bookToRemove);
                    }
                }
            }
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.ID == id);
        }
    }
}
