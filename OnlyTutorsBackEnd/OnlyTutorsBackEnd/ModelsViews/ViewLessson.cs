using OnlyTutorsBackEnd.ModelsViews;

namespace OnlyTutorsBackEnd.Models
{
    public class ViewLessson : Lesson
    {
        public string Subjectname { get; set; }
        public List<ViewStudent> Students { get; set; }
    }
}
