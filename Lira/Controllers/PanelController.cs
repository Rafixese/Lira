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
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Panel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Panel.Include(p => p.Board);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Panel/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .Include(p => p.Board)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // GET: Panel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Panel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BoardId")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                panel.Id = Guid.NewGuid();
                _context.Add(panel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Board", new {id = panel.BoardId});
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "Id", "Id", panel.BoardId);
            return View(panel);
        }

        // GET: Panel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel.FindAsync(id);
            if (panel == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "Id", "Id", panel.BoardId);
            return View(panel);
        }

        // POST: Panel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,BoardId")] Panel panel)
        {
            if (id != panel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelExists(panel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Board", new {id = panel.BoardId});
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "Id", "Id", panel.BoardId);
            return View(panel);
        }

        // GET: Panel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .Include(p => p.Board)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // POST: Panel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var panel = await _context.Panel.FindAsync(id);
            _context.Panel.Remove(panel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Board", new {id = panel.BoardId});
        }

        private bool PanelExists(Guid id)
        {
            return _context.Panel.Any(e => e.Id == id);
        }
    }
}
