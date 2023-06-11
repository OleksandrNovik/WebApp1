using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
	public class SignUpViewModel
	{
		[Required]
		public string UserName { get; set; }
		
		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string LastName { get; set; }
		
		[Required]
		public string Email { get; set; }
		public string? About { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 8, 
			ErrorMessage = "Пароль повинен складатися з від 8 до 20 символів")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%_-]).{8,}$", 
			ErrorMessage = "Пароль повинен містити хоча б 1 велику, 1 малу літеру, 1 цифру та 1 унікальний символ (@#$%_-)")]
		public string Password { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Пароль не співпадає")]
		public string ComfirmPassword { get; set; }
		public UserRole Role { get; set; }
	}
}
