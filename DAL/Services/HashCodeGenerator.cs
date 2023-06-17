using BLL.Injections;
using System.Security.Cryptography;
using System.Text;

namespace DAL.Services
{
	public class HashCodeGenerator : IHashCodeGenerator
	{
		private static readonly SHA256 hashGenerator = SHA256.Create();
		/// <summary>
		/// Генерація хешу для пароля 
		/// </summary>
		/// <param name="password"> Пароль користувача</param>
		/// <returns> Хешовану версію пароля </returns>
		public string GenerateHash(string password)
		{
			var passwordBytes = Encoding.Default.GetBytes(password);
			var hashedBytes = hashGenerator.ComputeHash(passwordBytes);
			return Convert.ToHexString(hashedBytes);
		}
	}
}
