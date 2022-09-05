using UnityEngine;

namespace LoginHexagonal
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private ViewsManager viewsManager = null;

		private IAuthPort authPort;

		private void Start()
		{
			InitializeAdapters();
			InitializeBOs();
			InitializeViewManager();

			viewsManager.ShowMainPage();
		}

		private void InitializeAdapters()
		{
			authPort = new GenericAuthAdapter();
		}

		private void InitializeBOs()
		{
			BusinessObjectLocator.AddBO(new LoginBO(authPort));
			BusinessObjectLocator.AddBO(new RegisterBO(authPort));
		}

		private void InitializeViewManager()
		{
			viewsManager.InitializeViews();
		}
	}
}