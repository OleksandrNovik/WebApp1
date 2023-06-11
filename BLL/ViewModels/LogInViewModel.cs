using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
	public class LogInViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
