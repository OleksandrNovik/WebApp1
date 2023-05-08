using BLL.Educational_entities.Organization;

namespace BLL.ViewModels
{
    public class CourseInfoViewModel
    {
        #region Course info
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public bool IsPublic { get; set; }
        #endregion
        #region Author info
        public string AuthorNickName { get; set; }
        #endregion
        #region Course options info
        public CourseLevel LevelInfo { get; set; }
        public CourseType TypeInfo { get; set; }
        public string[]? AdditionalInfo { get; set; }
        #endregion
    }
}
