using BLL.Educational_entities.Organization;
using BLL.ViewModels;

namespace BLL.Injections
{
	public interface ICourseControllerHelper
	{
		public List<CourseInfoViewModel> SetModel(List<Courses> courses);
	}
}
