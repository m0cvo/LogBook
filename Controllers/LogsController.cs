using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogBook.Data;
using LogBook.Models;

namespace LogBook.Controllers
{
    public class LogsController : Controller
    {
        private readonly LogBookContext _context;

        public LogsController(LogBookContext context)
        {
            _context = context;
        }

        // GET: Logs
        public async Task<IActionResult> Index(string searchString)
        {
            var logs = from m in _context.Log select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(s => s.Callsign!.Contains(searchString));
            }
            return View(await logs.ToListAsync());
              //return _context.Log != null ? 
              //            View(await _context.Log.ToListAsync()) :
              //            Problem("Entity set 'LogBookContext.Log'  is null.");
        }

        // GET: Logs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .FirstOrDefaultAsync(m => m.Id == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // GET: Logs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Time,Frequency,Mode,Power,Callsign,ReportSent,ReportReceived")] Log log)
        {
            if (ModelState.IsValid)
            {
                _context.Add(log);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // GET: Logs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time,Frequency,Mode,Power,Callsign,ReportSent,ReportReceived")] Log log)
        {
            if (id != log.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(log);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogExists(log.Id))
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
            return View(log);
        }

        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .FirstOrDefaultAsync(m => m.Id == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Log == null)
            {
                return Problem("Entity set 'LogBookContext.Log'  is null.");
            }
            var log = await _context.Log.FindAsync(id);
            if (log != null)
            {
                _context.Log.Remove(log);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogExists(int id)
        {
          return (_context.Log?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
