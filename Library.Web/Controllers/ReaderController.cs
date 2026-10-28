using Business.Interfaces;
using Library.Web.ViewModels.Reader;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        public async Task<IActionResult> Index()
        {
            var readers = await _readerService.GetAllAsync();

            var model = new List<ReaderIndexViewModel>();

            foreach (var reader in readers)
            {
                var borrowedCount = await _readerService.GetBorrowedBookCountAsync(reader.Id);
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
            var reader = await _readerService.GetWithBooksAsync(id);

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var reader = new Reader
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                RegistrationDate = model.RegistrationDate
            };

            await _readerService.CreateAsync(reader);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reader = await _readerService.GetByIdAsync(id);

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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var reader = await _readerService.GetByIdAsync(id);

            if (reader == null)
            {
                return NotFound();
            }

            reader.FirstName = model.FirstName;
            reader.LastName = model.LastName;
            reader.Email = model.Email;
            reader.PhoneNumber = model.PhoneNumber;
            reader.RegistrationDate = model.RegistrationDate;

            await _readerService.UpdateAsync(reader);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reader = await _readerService.GetByIdAsync(id);

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
            await _readerService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
