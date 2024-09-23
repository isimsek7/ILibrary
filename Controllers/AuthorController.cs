using System;
using BookInfoApp.Controllers;
using BookInfoApp.Entities;
using BookInfoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookInfoApp.Controllers
{
    public class AuthorController : Controller
    {   // To mimick a database
        List<Author> _authors = BookController._authors;
        List<Book> _books = BookController._books;

        public IActionResult List()
        {
        // Only displaying name and birth dates were not enough so we show the books too 
            var authorViewModels = _authors.Select(a => new AuthorViewModel
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}",
                DateOfBirth = a.DateOfBirth,
                BookTitles = _books.Where(b => b.AuthorId == a.Id).Select(b => b.Title).ToList()
            }).ToList();

            return View(authorViewModels);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            // Find the author by id from the _authors list
            var author = _authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            // Create an AuthorViewModel with the found author data
            var authorViewModel = new AuthorViewModel
            {
                Id = author.Id,
                FullName = $"{author.FirstName} {author.LastName}",
                DateOfBirth = author.DateOfBirth
            };

            return View(authorViewModel);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the author by id
                var author = _authors.FirstOrDefault(a => a.Id == model.Id);

                if (author == null)
                {
                    return NotFound();
                }

                // Update the author's data
                var names = model.FullName.Split(' ');
                author.FirstName = names.FirstOrDefault();
                author.LastName = string.Join(" ", names.Skip(1)); //This was a bit unncesary but i wanted to see the result
                author.DateOfBirth = model.DateOfBirth;            //But i still do think that there is an easier way of doing this.

                // Redirect to the list view after editing
                return RedirectToAction("List");
            }

            // Return to the view with validation errors if the model state is invalid
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            // Mark the author as deleted (soft delete)
            author.IsDeleted = true;

            // Redirect back to the list after soft deletion
            return RedirectToAction("List");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            // Return the empty form for author creation
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Author model)
        {
            if (ModelState.IsValid)
            {
                // Generate a new Id for the author
                int newId = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1;

                // Create the new author entity
                var newAuthor = new Author
                {
                    Id = newId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    IsDeleted = false // Ensure new author is not marked as deleted
                };

                // Add the new author to the list
                _authors.Add(newAuthor);

                // Redirect to the list view after successful creation
                return RedirectToAction("List");
            }

            // If the model state is invalid, return the form with validation errors
            return View(model);
        }


    }
}


