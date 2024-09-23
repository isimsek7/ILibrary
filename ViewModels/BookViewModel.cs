using System;
using BookInfoApp.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookInfoApp.ViewModels
{
    public class BookViewModel
    {

        public int Id { get; set; }
        [BindRequired]
        public string Title { get; set; }
        [BindRequired]
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }
        public string? CoverImageUrl { get; set; }
        [BindNever]
        public string? AuthorName { get; set; }
    }
}
