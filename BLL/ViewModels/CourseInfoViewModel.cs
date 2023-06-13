using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class CourseInfoViewModel
    {
        // Частина для валідації
        public int CourseId { get; set; }

		[Required(ErrorMessage = "Назва курсу не повинна бути порожньою")]
		public string CourseName { get; set; }
		
        [UIHint("MultilineText")]
		public string? CourseDescription { get; set; }

        // Частина для показу 
        public Courses? CourseInfo { get; set; }
        public IEnumerable<Topic>? Topics { get; set; }
        public string AuthorNickName { get; set; }
        public int AuthorId { get; set; }
        public CourseOptions? Options { get; set; }
    }
}
