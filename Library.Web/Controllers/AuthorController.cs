using Business.Interfaces;
using Library.Web.ViewModels.Author;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();

            var model = new List<AuthorIndexViewModel>();

            foreach (var author in authors)
            {
                var booksCount = await _authorService.GetBookCountByAuthorAsync(author.Id);
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
            var author = await _authorService.GetWithBooksAsync(id);

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var author = new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Biography = model.Biography,
                BirthDate = model.BirthDate
            };

            await _authorService.CreateAsync(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.Biography = model.Biography;
            author.BirthDate = model.BirthDate;

            await _authorService.UpdateAsync(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

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
            await _authorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
