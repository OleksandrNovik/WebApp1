namespace BLL.Injections
{
	public interface IHashCodeGenerator
	{
		public string GenerateHash(string password);
	}
}
