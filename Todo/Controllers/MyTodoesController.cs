using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    public class MyTodoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyTodoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyTodoes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyTodo.ToListAsync());
        }

        // GET: MyTodoes/Search
        [Authorize]
        public async Task<IActionResult> ShowSearch()
        {
            return View();
        }
        // POST: MyTodoes/Search
        public async Task<IActionResult> ShowSearchresults( string SearchPhrase)
        {
            return View("Index", await _context.MyTodo.Where(j => j.Date.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: MyTodoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTodo = await _context.MyTodo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTodo == null)
            {
                return NotFound();
            }

            return View(myTodo);
        }

        // GET: MyTodoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyTodoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Activity,Time,Remark")] MyTodo myTodo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myTodo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myTodo);
        }

        // GET: MyTodoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTodo = await _context.MyTodo.FindAsync(id);
            if (myTodo == null)
            {
                return NotFound();
            }
            return View(myTodo);
        }

        // POST: MyTodoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Activity,Time,Remark")] MyTodo myTodo)
        {
            if (id != myTodo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myTodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyTodoExists(myTodo.Id))
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
            return View(myTodo);
        }

        // GET: MyTodoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTodo = await _context.MyTodo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTodo == null)
            {
                return NotFound();
            }

            return View(myTodo);
        }

        // POST: MyTodoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myTodo = await _context.MyTodo.FindAsync(id);
            _context.MyTodo.Remove(myTodo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyTodoExists(int id)
        {
            return _context.MyTodo.Any(e => e.Id == id);
        }
    }
}
