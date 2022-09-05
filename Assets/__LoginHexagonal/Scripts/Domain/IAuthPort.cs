namespace LoginHexagonal
{
	public interface IAuthPort
	{
		void Login(string username, string password);
		void Register(string username, string password);
	}
}