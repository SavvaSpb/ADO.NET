namespace Test_ADO.NET.Models
{
    public class Teacher
    {
        public int TeachersId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
    }
}