using BLL.Educational_entities.Organization;
using BLL.ViewModels;

namespace BLL.Injections
{
	public interface ICourseControllerHelper
	{
		/// <summary>
		/// Асинхронний метод створення моделі для представлення курсу
		/// </summary>
		/// <param name="courses"> Курс, який варто представити як модель </param>
		/// <returns> Модель створена на основі даного курсу</returns>
		public async Task<List<CourseInfoViewModel>> CreateModel(List<Courses> courses)
		{
			await Task.Delay(100);
			return new List<CourseInfoViewModel>();
		}
		/// <summary>
		/// Асинхронний метод визначення автора курсу
		/// </summary>
		/// <param name="course"> Курс автора якого треба знайти </param>
		/// <returns> Id автора курсу </returns>
		public async Task<int> GetAuthorId(Courses course)
		{
			await Task.Delay(100);
			return -1;
		}
		/// <summary>
		/// Дістаємо усю інформацію про курс, за його id
		/// </summary>
		/// <param name="id"> id курсу</param>
		/// <returns> Модель курсу створену на основі його даних </returns>
        public async Task<CourseInfoViewModel?> GetCourseDataById(int id)
        {
            await Task.Delay(1000);
            return null;
        }
    }
}
