namespace OnlyTutorsBackEnd.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; } 

        public int StudentId{ get; set; }

        public int TutorId { get; set; }
    }
}
