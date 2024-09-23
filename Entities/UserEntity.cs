using System;
namespace BookInfoApp.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }  // Unique identifier for the member

        public string FullName { get; set; }  // Member's full name

        public string Email { get; set; }  // Member's email address

        public string Password { get; set; }  // Member's login password

        public string PhoneNumber { get; set; }  // Member's phone number

        public DateTime JoinDate { get; set; }  // Member's registration date

    }
}

