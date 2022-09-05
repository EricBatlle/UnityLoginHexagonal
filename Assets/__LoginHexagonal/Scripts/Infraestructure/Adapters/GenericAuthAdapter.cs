using System;
using UnityEngine;

namespace LoginHexagonal
{
	public class GenericAuthAdapter : IAuthPort
	{
		public void Login(string username, string password)
		{
			if (password.Length < 6)
				throw new FormatException();

			Debug.Log("Login!");
		}

		public void Register(string username, string password)
		{
			if (password.Length < 6)
				throw new FormatException();

			Debug.Log("Register!");
		}
	}
}