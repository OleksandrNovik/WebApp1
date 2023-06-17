using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
	public class LogInViewModel
	{
		[Required (ErrorMessage = "Ім'я користувача є обов'язковим")]
		public string UserName { get; set; }

		[Required (ErrorMessage = "Пароль є обов'язковим")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
