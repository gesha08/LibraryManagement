using Business.Interfaces;
using Library.Web.ViewModels.Book;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();

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
            var book = await _bookService.GetWithDetailsAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var currentBorrow = book.BookReaders.FirstOrDefault(br => br.ReturnedDate == null);

            var availableReaders = (await _bookService.GetAvailableReadersAsync())
                .Select(r => new { r.Id, FullName = r.FirstName + " " + r.LastName })
                .ToList();

            var model = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Pages = book.Pages,
                PublicationYear = book.PublicationYear,
                AuthorName = book.Author.FirstName + " " + book.Author.LastName,
                AuthorBirthDate = book.Author.BirthDate,
                IsBorrowed = currentBorrow != null,
                CurrentBorrowerName = currentBorrow != null ? currentBorrow.Reader.FirstName + " " + currentBorrow.Reader.LastName : null,
                BorrowedDate = currentBorrow?.BorrowedDate,
                CurrentBorrowerId = currentBorrow?.ReaderId,
                AvailableReaders = new SelectList(availableReaders, "Id", "FullName")
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = new SelectList(
                await _bookService.GetAllAuthorsAsync(),
                "Id",
                "FirstName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(
                    await _bookService.GetAllAuthorsAsync(),
                    "Id",
                    "FirstName");

                return View(model);
            }

            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                ISBN = model.ISBN,
                Pages = model.Pages,
                PublicationYear = model.PublicationYear,
                AuthorId = model.AuthorId
            };

            await _bookService.CreateAsync(book);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

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
                await _bookService.GetAllAuthorsAsync(),
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

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(
                    await _bookService.GetAllAuthorsAsync(),
                    "Id",
                    "FirstName");

                return View(model);
            }

            var book = await _bookService.GetByIdAsync(id);

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

            await _bookService.UpdateAsync(book);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

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
            var book = await _bookService.GetByIdAsync(id);

            if (book != null)
            {
                await _bookService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        // Borrow a book
        public async Task<IActionResult> Borrow(int id)
        {
            var book = await _bookService.GetWithDetailsAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            if (book.BookReaders.Any(br => br.ReturnedDate == null))
            {
                TempData["Error"] = "This book is already borrowed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var availableReaders = (await _bookService.GetAvailableReadersAsync())
                .Select(r => new { r.Id, FullName = r.FirstName + " " + r.LastName })
                .ToList();

            ViewBag.Book = book;
            ViewBag.Readers = new SelectList(availableReaders, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Borrow(int id, int readerId)
        {
            var book = await _bookService.GetByIdAsync(id);
            var isBorrowed = await _bookService.IsBorrowedAsync(id);

            if (book == null || isBorrowed)
            {
                TempData["Error"] = "This book is already borrowed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            await _bookService.BorrowAsync(id, readerId);

            TempData["Success"] = $"Book '{book.Title}' borrowed.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // Return a book
        [HttpPost]
        public async Task<IActionResult> Return(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var isBorrowed = await _bookService.IsBorrowedAsync(id);

            if (!isBorrowed)
            {
                TempData["Error"] = "This book is not currently borrowed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            await _bookService.ReturnAsync(id);

            TempData["Success"] = $"Book '{book.Title}' returned.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
