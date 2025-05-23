namespace OnlyTutorsBackEnd.Models
{
    public class SubjectPricing
    {
        public int Id { get; set; }
        public double Price { get; set; }


        public int userId { get; set; }

        public int SubjectId { get; set; }

    }
}
