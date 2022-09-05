namespace LoginHexagonal
{
	public class RegisterBO : IBusinessObject
	{
		private IAuthPort authPort;

		public RegisterBO(IAuthPort authPort)
		{
			this.authPort = authPort;
		}

		public void Register(string username, string password)
		{
			authPort.Register(username, password);
		}
	}
}