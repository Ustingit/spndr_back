using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpndRr.Data;
using SpndRr.Models.Spends;

namespace SpndRr.Controllers
{
    public class SpendController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpendController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Spend
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spend.ToListAsync());
        }

        // GET: Spend/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spend == null)
            {
                return NotFound();
            }

            return View(spend);
        }

        // GET: Spend/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spend/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sum,Type,SubType,Date,Comment")] Spend spend)
        {
	        spend.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(spend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spend);
        }

        // GET: Spend/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend.FindAsync(id);
            if (spend == null)
            {
                return NotFound();
            }
            return View(spend);
        }

        // POST: Spend/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,Type,SubType,Date,Comment")] Spend spend)
        {
            if (id != spend.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpendExists(spend.Id))
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
            return View(spend);
        }

        // GET: Spend/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spend == null)
            {
                return NotFound();
            }

            return View(spend);
        }

        // POST: Spend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spend = await _context.Spend.FindAsync(id);
            _context.Spend.Remove(spend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpendExists(int id)
        {
            return _context.Spend.Any(e => e.Id == id);
        }
    }
}
