namespace LibraryManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public int BookId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal OverdueFine { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
