using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;

namespace BLL.ViewModels
{
    public class CourseInfoViewModel
    {
        public Courses CourseInfo { get; set; }
        public IEnumerable<Topic>? Topics { get; set; }
        public string AuthorNickName { get; set; }
        public int AuthorId { get; set; }
        public CourseOptions Options { get; set; }
    }
}
