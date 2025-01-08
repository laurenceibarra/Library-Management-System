namespace LibraryManagementSystem.Models
{
    public class Fine
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateApplied { get; set; }
    }
}
