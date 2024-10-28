using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_iqstevens.Data;
using Fall2024_Assignment3_iqstevens.Models;
using System.Text.RegularExpressions;

namespace Fall2024_Assignment3_iqstevens.Controllers
{
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actor.ToListAsync());
        }

        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            string mimeType = TempData["MimeType"]?.ToString() ?? "image/jpeg";
            ViewData["MimeType"] = mimeType;

            return View(actor);
        }

        // GET: Actor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,IMDB,Photo")] Actor actor, IFormFile Photo)
        {
            if (!IsValidIMDBLink(actor.IMDB))
            {
                ModelState.AddModelError("IMDB", "The IMDB field must be a valid IMDB URL.");
            }

            if (ModelState.IsValid)
            {
                if (Photo != null || Photo!.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    Photo.CopyTo(memoryStream);
                    actor.Photo = memoryStream.ToArray();
                    var fileExtension = Path.GetExtension(Photo.FileName).ToLowerInvariant();
                    string mimeType = "image/jpeg"; // Default to JPEG

                    if (fileExtension == ".png")
                    {
                        mimeType = "image/png";
                    }
                    else if (fileExtension == ".jpeg" || fileExtension == ".jpg")
                    {
                        mimeType = "image/jpeg";
                    }

                    // Store the MIME type in TempData to use later
                    TempData["MimeType"] = mimeType;
                }


                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,IMDB,Photo")] Actor actor, IFormFile? Photo)
        {
            if (!IsValidIMDBLink(actor.IMDB))
            {
                ModelState.AddModelError("IMDB", "The IMDB field must be a valid IMDB URL.");
            }

            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await Photo.CopyToAsync(memoryStream);
                    actor.Photo = memoryStream.ToArray();

                    var fileExtension = Path.GetExtension(Photo.FileName).ToLowerInvariant();
                    string mimeType = "image/jpeg"; 

                    if (fileExtension == ".png")
                    {
                        mimeType = "image/png";
                    }
                    else if (fileExtension == ".jpeg" || fileExtension == ".jpg")
                    {
                        mimeType = "image/jpeg";
                    }

                    TempData["MimeType"] = mimeType;
                }

                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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
            return View(actor);
        }

        private bool IsValidIMDBLink(string imdbLink)
        {
            if (string.IsNullOrEmpty(imdbLink))
            {
                return false;
            }

            // Regex pattern for a valid IMDB URL
            var regex = new Regex(@"^https:\/\/(www\.)?imdb\.com\/name\/nm\d{7,8}\/?$");
            return regex.IsMatch(imdbLink);
        }

        // GET: Actor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            string mimeType = TempData["MimeType"]?.ToString() ?? "image/jpeg";
            ViewData["MimeType"] = mimeType;


            return View(actor);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor != null)
            {
                _context.Actor.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }
    }
}
