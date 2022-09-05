namespace LoginHexagonal
{
	public class LoginBO : IBusinessObject
	{
		private IAuthPort authPort;

		public LoginBO(IAuthPort authPort)
		{
			this.authPort = authPort;
		}

		public void Login(string username, string password)
		{
			authPort.Login(username, password);
		}
	}
}