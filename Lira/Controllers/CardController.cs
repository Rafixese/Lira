using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lira.Data;
using Lira.Models;
using Microsoft.AspNetCore.Http;

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
                var panel = _context.Panel.FindAsync(card.PanelId);
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Board", new {id = panel.Result.BoardId});
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
                var panel = _context.Panel.FindAsync(card.PanelId);
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
                return RedirectToAction("Details", "Board", new {id = panel.Result.BoardId});
            }
            ViewData["PanelId"] = new SelectList(_context.Panel, "Id", "Id", card.PanelId);
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> MoveCard([FromBody] CardMove cm)
        {
            var card = await _context.Card.FindAsync(cm.CardId);
            card.PanelId = cm.PanelId;
            _context.Update(card);
            await _context.SaveChangesAsync();
            return Ok();
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
            var panel = _context.Panel.FindAsync(card.PanelId);
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Board", new {id = panel.Result.BoardId});
        }

        private bool CardExists(Guid id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
