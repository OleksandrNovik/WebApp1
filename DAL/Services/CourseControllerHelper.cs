using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.ViewModels;

namespace DAL.Services
{
	public class CourseControllerHelper : ICourseControllerHelper
	{
		public List<CourseInfoViewModel> SetModel(List<Courses> courses)
		{
			var list = new List<CourseInfoViewModel>();
			foreach (var course in courses)
			{
				list.Add(new CourseInfoViewModel
				{
					CourseInfo = course,
					AuthorNickName = "primeMentor12",
					Options = course.Options,
				});
			}
			return list;
		}
	}
}
