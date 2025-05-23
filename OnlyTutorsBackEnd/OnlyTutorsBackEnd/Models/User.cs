namespace OnlyTutorsBackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth{ get; set; }
        public float Rating { get; set; }
        public string ImagePath { get; set; }
    }
}
