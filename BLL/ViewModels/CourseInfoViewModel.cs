using BLL.Educational_entities.Organization;

namespace BLL.ViewModels
{
    public class CourseInfoViewModel
    {
        public Courses CourseInfo { get; set; }
        public string AuthorNickName { get; set; }
        public CourseOptions Options { get; set; }
    }
}
