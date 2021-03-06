using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Note.ToListAsync());
        }


        // GET: Notes/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

       


        // GET: Notes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Note.Where( j => j.NoteTitle.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        
        [HttpGet]
        public async Task<IActionResult> DetailsPartial(int id)
        {
            var note = _context.Note.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("DetailsPartial", note);
        }



        //  Get: Note/AddOrEdit`
        //  Get: Note/AddOrEdit/5
        public  async Task<IActionResult>  AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                var note = await _context.Note.FindAsync(id);
                if(note  == null)
                {
                    return NotFound();
                }
                return View(note);
            }
        }








        // GET: Notes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoteTitle,NoteBody,User,Priority,Resolved")] Note note)
        {
            if(note.NoteTitle == note.NoteBody)
            {
                ModelState.AddModelError("NoteTitle", "Issue Subject and Explanation can not be identical");
            }
            if (ModelState.IsValid)  
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                TempData["created"] = $"Issue '{note.NoteTitle}' Created";
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoteTitle,NoteBody,User,Priority,Resolved")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                    TempData["edited"] = $"Issue '{note.NoteTitle}' has been updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
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
            return View(note);
        }

        // GET: Notes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            TempData["deleted"] = $"Issue '{note.NoteTitle}' Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
