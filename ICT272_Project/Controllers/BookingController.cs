using ICT272_Project.Data;
using ICT272_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ICT272_Project.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _dbContext;
        public BookingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var bookings = await _dbContext.Bookings
                .Include(b => b.Tourist)
                .Include(b => b.TourDate)
                .ThenInclude(td => td.TourPackage)
                .ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _dbContext.Bookings
                .Include(b => b.Tourist)
                .Include(b => b.TourDate)
                .ThenInclude(td => td.TourPackage)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,TouristID,TourDateID,BookingDate,Status,PaymentStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(booking);
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Booking created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["TouristID"] = new SelectList(_dbContext.Tourists, "TouristID", "FullName", booking.TouristID);
            ViewData["TourDateID"] = new SelectList(_dbContext.TourDates.Include(td => td.TourPackage),
                "TourDateID", "TourPackage.PackageName", booking.TourDateID);
            return View(booking);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["TouristID"] = new SelectList(_dbContext.Tourists, "TouristID", "FullName");
            ViewData["TourDateID"] = new SelectList(_dbContext.TourDates.Include(td => td.TourPackage),
                "TourDateID", "TourPackage.PackageName");
            return View();
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _dbContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["TouristID"] = new SelectList(_dbContext.Tourists, "TouristID", "FullName", booking.TouristID);
            ViewData["TourDateID"] = new SelectList(_dbContext.TourDates.Include(td => td.TourPackage),
                "TourDateID", "TourPackage.PackageName", booking.TourDateID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,TouristID,TourDateID,BookingDate,Status,PaymentStatus")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(booking);
                    await _dbContext.SaveChangesAsync();
                    TempData["Success"] = "Booking updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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

            ViewData["TouristID"] = new SelectList(_dbContext.Tourists, "TouristID", "FullName", booking.TouristID);
            ViewData["TourDateID"] = new SelectList(_dbContext.TourDates.Include(td => td.TourPackage),
                "TourDateID", "TourPackage.PackageName", booking.TourDateID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _dbContext.Bookings
                .Include(b => b.Tourist)
                .Include(b => b.TourDate)
                .ThenInclude(td => td.TourPackage)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var booking = await _dbContext.Bookings
                    .Include(b => b.Feedbacks)
                    .FirstOrDefaultAsync(b => b.BookingID == id);

                if (booking != null)
                {
                    // Remove related feedbacks first
                    if (booking.Feedbacks != null && booking.Feedbacks.Any())
                    {
                        _dbContext.Feedbacks.RemoveRange(booking.Feedbacks);
                    }

                    _dbContext.Bookings.Remove(booking);
                    await _dbContext.SaveChangesAsync();
                    TempData["Success"] = "Booking deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the booking. Please try again.";
                // Log the exception here if you have logging configured
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownLists(Booking booking = null)
        {
            var tourists = await _dbContext.Tourists.ToListAsync();
            var tourDates = await _dbContext.TourDates
                .Include(td => td.TourPackage)
                .Where(td => td.Status == "Available")
                .ToListAsync();

            ViewData["TouristID"] = new SelectList(tourists, "TouristID", "FullName", booking?.TouristID);

            // Create custom display for tour dates
            var tourDateItems = tourDates.Select(td => new SelectListItem
            {
                Value = td.TourDateID.ToString(),
                Text = $"{td.TourPackage.Title} - {td.AvailableDate:MMM dd, yyyy}",
                Selected = booking?.TourDateID == td.TourDateID
            }).ToList();

            ViewData["TourDateID"] = tourDateItems;
        }

        private bool BookingExists(int id)
        {
            return _dbContext.Bookings.Any(e => e.BookingID == id);
        }
    }
}
