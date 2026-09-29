using Library.Data;
using Library.Web.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext context;

        public BookController(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await context.Books
                .Include(b => b.Author)
                .ToListAsync();

            var model = new List<BookIndexViewModel>();

            foreach (var book in books)
            {
                model.Add(new BookIndexViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Pages = book.Pages,
                    PublicationYear = book.PublicationYear,
                    AuthorName = book.Author.FirstName + " " + book.Author.LastName
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Pages = book.Pages,
                PublicationYear = book.PublicationYear,
                AuthorName = book.Author.FirstName + " " + book.Author.LastName,
                AuthorBirthDate = book.Author.BirthDate
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = new SelectList(
                await context.Authors.ToListAsync(),
                "Id",
                "FirstName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Library.Data.Entities.Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    ISBN = model.ISBN,
                    Pages = model.Pages,
                    PublicationYear = model.PublicationYear,
                    AuthorId = model.AuthorId
                };

                context.Books.Add(book);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = new SelectList(
                await context.Authors.ToListAsync(),
                "Id",
                "FirstName");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookEditViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Pages = book.Pages,
                PublicationYear = book.PublicationYear,
                AuthorId = book.AuthorId
            };

            ViewBag.Authors = new SelectList(
                await context.Authors.ToListAsync(),
                "Id",
                "FirstName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await context.Books.FindAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                book.Title = model.Title;
                book.Description = model.Description;
                book.ISBN = model.ISBN;
                book.Pages = model.Pages;
                book.PublicationYear = model.PublicationYear;
                book.AuthorId = model.AuthorId;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = new SelectList(
                await context.Authors.ToListAsync(),
                "Id",
                "FirstName");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookDeleteViewModel
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.FirstName + " " + book.Author.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}