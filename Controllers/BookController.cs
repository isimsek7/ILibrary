using Microsoft.AspNetCore.Mvc;
using BookInfoApp.Entities;
using static System.Net.WebRequestMethods;
using BookInfoApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BookInfoApp.Controllers
{
    //We populate our lists
    public class BookController : Controller
    {
        public static List<Author> _authors = new List<Author>
        {
        new Author { Id = 1, FirstName = "F. Scott", LastName = "Fitzgerald", DateOfBirth = new DateTime(1896, 9, 24) },
        new Author { Id = 2, FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25) },
        new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28) },
        new Author { Id = 4, FirstName = "J.D.", LastName = "Salinger", DateOfBirth = new DateTime(1919, 1, 1) },
        new Author { Id = 5, FirstName = "Herman", LastName = "Melville", DateOfBirth = new DateTime(1819, 8, 1) },
        new Author { Id = 6, FirstName = "Jane", LastName = "Austen", DateOfBirth = new DateTime(1775, 12, 16) },
        new Author { Id = 7, FirstName = "J.R.R.", LastName = "Tolkien", DateOfBirth = new DateTime(1892, 1, 3) },
        new Author { Id = 8, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31) },
        new Author { Id = 9, FirstName = "Leo", LastName = "Tolstoy", DateOfBirth = new DateTime(1828, 9, 9) },
        new Author { Id = 10, FirstName = "Homer", LastName = "", DateOfBirth = new DateTime(1, 1, 1) }
        };

    
        public static List<Book> _books = new List<Book>
    {
        new Book {
            Id = 1, Title = "The Great Gatsby",
            AuthorId = 1, Genre = "Fiction",
            PublishDate = new DateTime(1925, 4, 10), ISBN = "9780743273565",
            CopiesAvailable = 5, IsDeleted = false, CoverImageUrl="https://m.media-amazon.com/images/I/61dRoDRubtL._SL1500_.jpg" },
        new Book { Id = 2, Title = "1984",
            AuthorId = 2, Genre = "Dystopian",
            PublishDate = new DateTime(1949, 6, 8), ISBN = "9780451524935",
            CopiesAvailable = 8, IsDeleted = false, CoverImageUrl="https://m.media-amazon.com/images/I/81StSOpmkjL._SL1500_.jpg"},
        new Book
            {
                Id = 3,
                Title = "To Kill a Mockingbird",
                AuthorId = 3,
                Genre = "Fiction",
                PublishDate = new DateTime(1960, 7, 11),
                ISBN = "9780061120084",
                CopiesAvailable = 3,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/71FxgtFKcQL._SL1500_.jpg"

            },
            new Book
            {
                Id = 4,
                Title = "The Catcher in the Rye",
                AuthorId = 4,
                Genre = "Fiction",
                PublishDate = new DateTime(1951, 7, 16),
                ISBN = "9780316769488",
                CopiesAvailable = 4,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/8125BDk3l9L._SL1500_.jpg"
            },
            new Book
            {
                Id = 5,
                Title = "Moby-Dick",
                AuthorId = 5,
                Genre = "Adventure",
                PublishDate = new DateTime(1851, 10, 18),
                ISBN = "9781503280786",
                CopiesAvailable = 6,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/51HhY3G4UJL._SL1200_.jpg"
            },
            new Book
            {
                Id = 6,
                Title = "Pride and Prejudice",
                AuthorId = 6,
                Genre = "Romance",
                PublishDate = new DateTime(1813, 1, 28),
                ISBN = "9781503290563",
                CopiesAvailable = 8,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/61sGe1S2BaL._SL1200_.jpg"
            },
            new Book
            {
                Id = 7,
                Title = "The Lord of the Rings",
                AuthorId = 7,
                Genre = "Fantasy",
                PublishDate = new DateTime(1954, 7, 29),
                ISBN = "9780618640157",
                CopiesAvailable = 9,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/913sMwNHsBL._SL1500_.jpg"
            },
            new Book
            {
                Id = 8,
                Title = "Harry Potter and the Sorcerer's Stone",
                AuthorId = 8,
                Genre = "Fantasy",
                PublishDate = new DateTime(1997, 6, 26),
                ISBN = "9780439708180",
                CopiesAvailable = 10,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/91wKDODkgWL._SL1500_.jpg"

            },
            new Book
            {
                Id = 9,
                Title = "War and Peace",
                AuthorId = 9,
                Genre = "Historical Fiction",
                PublishDate = new DateTime(1869, 1, 1),
                ISBN = "9780199232765",
                CopiesAvailable = 2,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/71wXZB-VtBL._SL1200_.jpg"
            },
            new Book
            {
                Id = 10,
                Title = "The Odyssey",
                AuthorId = 10,
                Genre = "Epic",
                PublishDate = new DateTime(1, 1, 1),
                ISBN = "9780140268867",
                CopiesAvailable = 7,
                IsDeleted = false,
                CoverImageUrl="https://m.media-amazon.com/images/I/71RZuClkzaL._SL1200_.jpg"
            }
        };
        //Here's our ViewModel list for join action, I am using this practice because of the task's requirements
        //I would rather, join the books and authors completely and create one viewmodel and transfer data to their respective list

        private IEnumerable<BookViewModel> BooksWithAuthors()
        {
            return _books
                .Where(b => !b.IsDeleted)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId, 
                    AuthorName = _authors
                        .Where(a => a.Id == b.AuthorId)
                        .Select(a => a.FirstName + " " + a.LastName)
                        .FirstOrDefault() ?? "Unknown Author", // Default value if no match found
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    ISBN = b.ISBN,
                    CopiesAvailable = b.CopiesAvailable,
                    CoverImageUrl = b.CoverImageUrl,
                })
                .ToList();
        }

        //CRUD's
        public IActionResult List()
        {
            var booksWithAuthors = BooksWithAuthors();
            return View(booksWithAuthors);
        }

        //Id is hidden in the card, so it transfers us to /Details/id page
        public IActionResult Details(int id)
        {
            var book = BooksWithAuthors().FirstOrDefault(b => b.AuthorId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //Create action is using the max id plus one when opening the form
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            int nextAuthorId = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            ViewBag.NextAuthorId = nextAuthorId;
            return View();
        }

        //Again that id is now our newly created book id and then we just fill the pocos
        [HttpPost]
        [Authorize]
        public IActionResult Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                int newId = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
                // Create a new Book object from the model
                var newBook = new Book
                {
                    Id = newId,
                    AuthorId = newId,
                    Title = model.Title,
                    Genre = model.Genre,
                    PublishDate = model.PublishDate,
                    ISBN = model.ISBN,
                    CopiesAvailable = model.CopiesAvailable,
                    CoverImageUrl = model.CoverImageUrl
                };

                // Add the new book to your list or database
                _books.Add(newBook);

                // Redirect to the list view after creation
                return RedirectToAction("List");
            }

            // Return to the view with validation errors if the model state is invalid
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            // Find the book to be edited
            var book = _books.FirstOrDefault(b => b.Id == id && !b.IsDeleted);
            if (book == null)
            {
                return NotFound();
            }

            // Generate the list of authors for the dropdown, we are basically sending the list items, it iterates over and the results
            //are shown via dropdown
            var authors = _authors.Select(a => new
            {
                Id = a.Id,
                FullName = a.FirstName + " " + a.LastName
            }).ToList();

            // Create the BookViewModel and set the properties
            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId, // This will pre-select the author in the dropdown, so basically we need to create author first to select the author of the book
                AuthorName = authors.FirstOrDefault(a => a.Id == book.AuthorId)?.FullName, // For display-only if needed
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                CoverImageUrl = book.CoverImageUrl
            };

            // Pass the list of authors to the view
            ViewBag.Authors = new SelectList(authors, "Id", "FullName", book.AuthorId); // Pre-select the current author

            return View(bookViewModel); // Return the view with the model and the authors dropdown
        }

        //This edit actions possibly have an easier way
        [HttpPost]
        [Authorize]
        public IActionResult Edit(BookViewModel model)
        {
            // Populate ViewBag.Authors for the dropdown (for invalid model case)
            var authors = _authors.Select(a => new
            {
                Id = a.Id,
                FullName = a.FirstName + " " + a.LastName
            }).ToList();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName", model.AuthorId); // Pre-select chosen author

            if (ModelState.IsValid)
            {
                var book = _books.FirstOrDefault(b => b.Id == model.Id && !b.IsDeleted);
                if (book == null)
                {
                    return NotFound();
                }

                // Update the book's properties with the data from the form
                book.Title = model.Title;
                book.AuthorId = model.AuthorId;
                book.Genre = model.Genre;
                book.PublishDate = model.PublishDate;
                book.ISBN = model.ISBN;
                book.CopiesAvailable = model.CopiesAvailable;
                book.CoverImageUrl = model.CoverImageUrl;

               
                return RedirectToAction("List");
            }

            // If the model state is invalid, return the view with the same model and dropdown populated
            return View(model);
        }
        //DeleteConfirmed yerine Delete yazili kalmasi icin action name verdik
        [HttpPost, ActionName("Delete")]
        [Authorize]
        //Id'sindeki kitabi soft delete yapiyoruz, Book/DeleteConfirmed/{id} ile
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }

            book.IsDeleted = true; //Soft Delete
            return RedirectToAction("List");
        }
    }
}


