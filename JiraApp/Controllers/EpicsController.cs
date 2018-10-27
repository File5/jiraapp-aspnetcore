using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JiraApp.Models;

namespace JiraApp.Controllers
{
    public class EpicsController : Controller
    {
        private readonly JiraContext _context;

        public EpicsController(JiraContext context)
        {
            _context = context;
        }

        // GET: Epics
        public async Task<IActionResult> Index()
        {
            var jiraContext = _context.Epics.Include(e => e.Project);
            return View(await jiraContext.ToListAsync());
        }

        // GET: Epics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epic = await _context.Epics
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epic == null)
            {
                return NotFound();
            }

            return View(epic);
        }

        // GET: Epics/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: Epics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProjectId")] Epic epic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", epic.ProjectId);
            return View(epic);
        }

        // GET: Epics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epic = await _context.Epics.FindAsync(id);
            if (epic == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", epic.ProjectId);
            return View(epic);
        }

        // POST: Epics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProjectId")] Epic epic)
        {
            if (id != epic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpicExists(epic.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", epic.ProjectId);
            return View(epic);
        }

        // GET: Epics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epic = await _context.Epics
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epic == null)
            {
                return NotFound();
            }

            return View(epic);
        }

        // POST: Epics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var epic = await _context.Epics.FindAsync(id);
            _context.Epics.Remove(epic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpicExists(int id)
        {
            return _context.Epics.Any(e => e.Id == id);
        }
    }
}
