namespace Hospital_Management_system.Models.Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}