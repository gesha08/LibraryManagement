using Library.Data;
using Library.Web.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext context;

        public AuthorController(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await context.Authors.ToListAsync();

            var model = new List<AuthorIndexViewModel>();

            foreach (var author in authors)
            {
                var booksCount = await context.Books.CountAsync(b => b.AuthorId == author.Id);
                model.Add(new AuthorIndexViewModel
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    BirthDate = author.BirthDate,
                    BooksCount = booksCount
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new AuthorDetailsViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                Books = author.Books.Select(b => new BookSimpleViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublicationYear = b.PublicationYear
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new Library.Data.Entities.Author
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Biography = model.Biography,
                    BirthDate = model.BirthDate
                };

                context.Authors.Add(author);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new AuthorEditViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Biography = author.Biography,
                BirthDate = author.BirthDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var author = await context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
                author.Biography = model.Biography;
                author.BirthDate = model.BirthDate;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new AuthorDeleteViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await context.Authors.FindAsync(id);

            if (author != null)
            {
                context.Authors.Remove(author);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}