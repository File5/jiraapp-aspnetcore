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
    public class TasksController : Controller
    {
        private readonly JiraContext _context;

        public TasksController(JiraContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var jiraContext = _context.Tasks.Include(t => t.Epic).Include(t => t.Person).Include(t => t.Project).Include(t => t.Status);
            return View(await jiraContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Epic)
                .Include(t => t.Person)
                .Include(t => t.Project)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["EpicId"] = new SelectList(_context.Epics, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedAt,ClosedAt,EpicId,ProjectId,StatusId,PersonId")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpicId"] = new SelectList(_context.Epics, "Id", "Id", task.EpicId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", task.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", task.StatusId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["EpicId"] = new SelectList(_context.Epics, "Id", "Id", task.EpicId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", task.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", task.StatusId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedAt,ClosedAt,EpicId,ProjectId,StatusId,PersonId")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["EpicId"] = new SelectList(_context.Epics, "Id", "Id", task.EpicId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", task.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", task.StatusId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Epic)
                .Include(t => t.Person)
                .Include(t => t.Project)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
