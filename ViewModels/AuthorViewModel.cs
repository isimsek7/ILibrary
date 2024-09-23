using System;
namespace BookInfoApp.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } 
        public DateTime DateOfBirth { get; set; }
        
        public List<string>? BookTitles { get; set; }

    }
}
    

