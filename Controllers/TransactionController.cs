using expenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace expenseTracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            /*ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");*/
            PopulateCategories();
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if(transaction.TransactionId == 0)
                    _context.Add(transaction);
                else
                    _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /*ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", transaction.CategoryId);*/
            PopulateCategories();
            return View(transaction);
        }
        /*
                // GET: Transaction/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null || _context.Transactions == null)
                    {
                        return NotFound();
                    }

                    var transaction = await _context.Transactions
                        .Include(t => t.Category)
                        .FirstOrDefaultAsync(m => m.TransactionId == id);
                    if (transaction == null)
                    {
                        return NotFound();
                    }

                    return View(transaction);
                }*/

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var categoryCollections = _context.Categories.ToList();
            Category defaultCategories = new Category() { CategoryId = 0, Title = "Choose a Category" };
            categoryCollections.Insert(0, defaultCategories);
            ViewBag.Categories = categoryCollections;
        }
    }
}
