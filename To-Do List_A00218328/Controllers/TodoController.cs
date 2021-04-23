using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_A00218328.DB;
using To_Do_List_A00218328.Models;

namespace To_Do_List_A00218328.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var TodoLists = _context.TodoLists;
            return View(await TodoLists.ToListAsync());
        }

        // GET: TodoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.TodoLists.FirstOrDefaultAsync(m => m.TodoItemId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: TodoLists/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: TodoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate")] TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                todo.DoneDate = DateTime.Now;
                todo.Done = false;
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(todo);
        }

        // GET: TodoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.TodoLists.FirstOrDefaultAsync(m => m.TodoItemId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: TodoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTodo([Bind("TodoItemId,Title,Description,DueDate")] TodoItem todo)
        {
            if (todo.TodoItemId == null)
            {
                return NotFound();
            }
            var todoToUpdate = await _context.TodoLists.FirstOrDefaultAsync(c => c.TodoItemId == todo.TodoItemId);
            try
            {
                todoToUpdate.UserEmail = User.Identity.Name;
                todoToUpdate.Title = todo.Title;
                todoToUpdate.Description = todo.Description;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: TodoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Todo = await _context.TodoLists.FirstOrDefaultAsync(m => m.TodoItemId == id);
            if (Todo == null)
            {
                return NotFound();
            }
            return View(Todo);
        }

        // POST: TodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.TodoLists.FindAsync(id);
            _context.TodoLists.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DoneTodo(int TodoId,bool r=false)
        {
            var todo = await _context.TodoLists.FindAsync(TodoId);
            todo.DoneDate = DateTime.Now;
            todo.Done = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
