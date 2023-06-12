using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
	public class SignUpViewModel
	{
		[Required (ErrorMessage = "Ім'я користувача є обов'язковим")]
		public string UserName { get; set; }
		
		[Required (ErrorMessage = "Введіть ваше ім'я (це обов'язкове поле)")]
		public string FirstName { get; set; }
		
		[Required (ErrorMessage = "Введіть ваше прізвище (це обов'язкове поле)")]
		public string LastName { get; set; }
		
		[Required (ErrorMessage = "Електронна пошта є обов'язковою")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[UIHint("MultilineText")]
		public string? About { get; set; }

		[Required (ErrorMessage = "Створення паролю є обов'язковою процедурою")]
		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 8, 
			ErrorMessage = "Пароль повинен складатися з від 8 до 20 символів")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%_-]).{8,}$", 
			ErrorMessage = "Пароль має містити великі малі букви, цифри та унікальні символи (@#$%_-)")]
		public string Password { get; set; }
		
		[Required (ErrorMessage = "Повторне введення паролю є обов'язковим")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Пароль не співпадає")]
		public string ComfirmPassword { get; set; }
		public UserRole Role { get; set; }
	}
}
