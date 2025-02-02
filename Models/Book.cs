﻿namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int YearPublished { get; set; }
        public int Copies { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
