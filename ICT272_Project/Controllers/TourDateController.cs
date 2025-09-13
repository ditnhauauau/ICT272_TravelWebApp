using ICT272_Project.Data;
using ICT272_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ICT272_Project.Controllers
{
    public class TourDateController : Controller
    {
        private readonly AppDbContext _context;
        public TourDateController(AppDbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<IActionResult> Index()
        {
            var tourDates = await _context.TourDates
                .Include(t => t.TourPackage)
                .ToListAsync();
            return View(tourDates);
        }

        // GET: TourDate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDate = await _context.TourDates
                .Include(t => t.TourPackage)
                .Include(t => t.Bookings)
                .ThenInclude(b => b.Tourist)
                .FirstOrDefaultAsync(m => m.TourDateID == id);

            if (tourDate == null)
            {
                return NotFound();
            }

            return View(tourDate);
        }

        // GET: TourDate/Create
        public IActionResult Create()
        {
            var packages = _context.TourPackages.ToList();
            ViewData["PackageID"] = new SelectList(packages, "PackageID", "PackageName");
            return View();
        }


        // POST: TourDate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourDateID,PackageID,AvailableDate,Status")] TourDate tourDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourDate);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tour date created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["PackageID"] = new SelectList(_context.TourPackages, "PackageID", "PackageName", tourDate.PackageID);
            return View(tourDate);
        }

        // GET: TourDate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDate = await _context.TourDates.FindAsync(id);
            if (tourDate == null)
            {
                return NotFound();
            }

            ViewData["PackageID"] = new SelectList(_context.TourPackages, "PackageID", "PackageName", tourDate.PackageID);
            return View(tourDate);
        }

        // POST: TourDate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourDateID,PackageID,AvailableDate,Status")] TourDate tourDate)
        {
            if (id != tourDate.TourDateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourDate);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Tour date updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourDateExists(tourDate.TourDateID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PackageID"] = new SelectList(_context.TourPackages, "PackageID", "PackageName", tourDate.PackageID);
            return View(tourDate);
        }

        // GET: TourDate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDate = await _context.TourDates
                .Include(t => t.TourPackage)
                .FirstOrDefaultAsync(m => m.TourDateID == id);

            if (tourDate == null)
            {
                return NotFound();
            }

            return View(tourDate);
        }

        // POST: TourDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDate = await _context.TourDates.FindAsync(id);
            if (tourDate != null)
            {
                _context.TourDates.Remove(tourDate);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tour date deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TourDateExists(int id)
        {
            return _context.TourDates.Any(e => e.TourDateID == id);
        }
    }
}
