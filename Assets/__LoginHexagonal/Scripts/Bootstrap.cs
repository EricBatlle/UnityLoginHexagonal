using System.Threading.Tasks;
using UnityEngine;

namespace LoginHexagonal
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private ViewsManager viewsManager = null;

		private ILocalizationPort localizationPort;
		private IAuthPort authPort;

		private async void Start()
		{
			await InitializeAdaptersAsync();
			InitializeBOs();
			InitializeViewManager();

			viewsManager.ShowMainPage();
		}

		private async Task InitializeAdaptersAsync()
		{
			localizationPort = await UnityLocalizationAdapter.CreateUnityLocalizationAdapterAsync();
			authPort = new GenericAuthAdapter();
		}

		private void InitializeBOs()
		{
			BusinessObjectLocator.AddBO(new LocalizationBO(localizationPort));
			BusinessObjectLocator.AddBO(new LoginBO(authPort));
			BusinessObjectLocator.AddBO(new RegisterBO(authPort));
		}

		private void InitializeViewManager()
		{
			viewsManager.InitializeViews();
		}
	}
}