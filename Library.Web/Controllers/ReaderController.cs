using Library.Data;
using Library.Web.ViewModels.Reader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
    public class ReaderController : Controller
    {
        private readonly LibraryDbContext context;

        public ReaderController(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var readers = await context.Readers.ToListAsync();

            var model = new List<ReaderIndexViewModel>();

            foreach (var reader in readers)
            {
                var borrowedCount = await context.BookReaders.CountAsync(br => br.ReaderId == reader.Id && br.ReturnedDate == null);
                model.Add(new ReaderIndexViewModel
                {
                    Id = reader.Id,
                    FirstName = reader.FirstName,
                    LastName = reader.LastName,
                    Email = reader.Email,
                    RegistrationDate = reader.RegistrationDate,
                    BooksBorrowedCount = borrowedCount
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reader = await context.Readers
                .Include(r => r.BookReaders)
                    .ThenInclude(br => br.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reader == null)
            {
                return NotFound();
            }

            var borrowedBooks = reader.BookReaders
                .Where(br => br.ReturnedDate == null)
                .Select(br => new BookSimpleViewModel
                {
                    Id = br.Book.Id,
                    Title = br.Book.Title,
                    PublicationYear = br.Book.PublicationYear
                })
                .ToList();

            var returnedBooks = reader.BookReaders
                .Where(br => br.ReturnedDate != null)
                .Select(br => new BookSimpleViewModel
                {
                    Id = br.Book.Id,
                    Title = br.Book.Title,
                    PublicationYear = br.Book.PublicationYear
                })
                .ToList();

            var model = new ReaderDetailsViewModel
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                PhoneNumber = reader.PhoneNumber,
                RegistrationDate = reader.RegistrationDate,
                BorrowedBooks = borrowedBooks,
                ReturnedBooks = returnedBooks
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReaderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reader = new Library.Data.Entities.Reader
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RegistrationDate = model.RegistrationDate
                };

                context.Readers.Add(reader);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reader = await context.Readers.FindAsync(id);

            if (reader == null)
            {
                return NotFound();
            }

            var model = new ReaderEditViewModel
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                PhoneNumber = reader.PhoneNumber,
                RegistrationDate = reader.RegistrationDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReaderEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var reader = await context.Readers.FindAsync(id);

                if (reader == null)
                {
                    return NotFound();
                }

                reader.FirstName = model.FirstName;
                reader.LastName = model.LastName;
                reader.Email = model.Email;
                reader.PhoneNumber = model.PhoneNumber;
                reader.RegistrationDate = model.RegistrationDate;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reader = await context.Readers.FindAsync(id);

            if (reader == null)
            {
                return NotFound();
            }

            var model = new ReaderDeleteViewModel
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reader = await context.Readers.FindAsync(id);

            if (reader != null)
            {
                context.Readers.Remove(reader);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}