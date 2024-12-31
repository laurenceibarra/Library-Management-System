namespace LibraryManagementSystem.Models
{
    public class Patron
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MembershipId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MembershipType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
