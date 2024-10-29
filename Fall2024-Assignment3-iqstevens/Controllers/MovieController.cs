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
using OpenAI.Chat;
using Azure.AI.OpenAI;
using Azure;

namespace Fall2024_Assignment3_iqstevens.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public MovieController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        // GET: Movie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var actors = await _context.MovieActor
                .Include(cs => cs.Actor)
                .Where(cs => cs.MovieId == movie.Id)
                .Select(cs => cs.Actor)
                .ToListAsync();


            //ChatClient client = new(model: "gpt-35-turbo", apiKey: _config["OpenAI:ApiKey"]);

            var endpoint = new Uri(_config["OpenAI:Endpoint"]);  // Azure OpenAI endpoint
            var apiKey = new AzureKeyCredential(_config["OpenAI:ApiKey"]);
            var openAiClient = new AzureOpenAIClient(endpoint, apiKey);

            var reviews = new List<string>();

            string prompt = $"Write 10 brief reviews based on different reviewer personas for the movie '{movie.Name}', each review separated by '|'. Include a fictional reviewer name at the beginning of each review.";


            var vm = new MovieDVM(movie, actors, reviews);

            string mimeType = TempData["MimeType"]?.ToString() ?? "image/jpeg";
            ViewData["MimeType"] = mimeType;

            return View(vm);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IMDB,Genre,ReleaseYear,Poster")] Movie movie, IFormFile? Poster)
        {
            if (!IsValidIMDBLink(movie.IMDB))
            {
                ModelState.AddModelError("IMDB", "The IMDB field must be a valid IMDB URL.");
            }


            if (ModelState.IsValid)
            {
                if (Poster != null && Poster!.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    Poster.CopyTo(memoryStream);
                    movie.Poster = memoryStream.ToArray();
                    var fileExtension = Path.GetExtension(Poster.FileName).ToLowerInvariant();
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

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IMDB,Genre,ReleaseYear,Poster")] Movie movie, IFormFile? Poster)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (!IsValidIMDBLink(movie.IMDB))
            {
                ModelState.AddModelError("IMDB", "The IMDB field must be a valid IMDB URL.");
            }

            if (ModelState.IsValid)
            {
                if (Poster != null && Poster.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await Poster.CopyToAsync(memoryStream);
                    movie.Poster = memoryStream.ToArray();

                    // Infer MIME type based on file extension
                    var fileExtension = Path.GetExtension(Poster.FileName).ToLowerInvariant();
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
                    try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        private bool IsValidIMDBLink(string imdbLink)
        {
            if (string.IsNullOrEmpty(imdbLink))
            {
                return false;
            }

            // Regex pattern for a valid IMDB URL
            var regex = new Regex(@"^https:\/\/(www\.)?imdb\.com\/title\/tt\d{7,8}\/?$");
            return regex.IsMatch(imdbLink);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            string mimeType = TempData["MimeType"]?.ToString() ?? "image/jpeg";
            ViewData["MimeType"] = mimeType;


            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
