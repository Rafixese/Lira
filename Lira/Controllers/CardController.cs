using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lira.Data;
using Lira.Models;

namespace Lira.Controllers
{
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Card
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Card.Include(c => c.Panel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Card/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.Panel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Card/Create
        public IActionResult Create()
        {
            ViewData["PanelId"] = new SelectList(_context.Panel, "Id", "Id");
            return View();
        }

        // POST: Card/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PanelId")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.Id = Guid.NewGuid();
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PanelId"] = new SelectList(_context.Panel, "Id", "Id", card.PanelId);
            return View(card);
        }

        // GET: Card/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["PanelId"] = new SelectList(_context.Panel, "Id", "Id", card.PanelId);
            return View(card);
        }

        // POST: Card/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,PanelId")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            ViewData["PanelId"] = new SelectList(_context.Panel, "Id", "Id", card.PanelId);
            return View(card);
        }

        // GET: Card/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.Panel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var card = await _context.Card.FindAsync(id);
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(Guid id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
