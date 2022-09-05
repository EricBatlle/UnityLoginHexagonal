using System.Collections.Generic;
using UnityEngine;

namespace LoginHexagonal
{
	public class ViewsManager : MonoBehaviour
	{
		[SerializeField] private LoginViewManager loginView = null;
		[SerializeField] private RegisterViewManager registerView = null;

		private List<IViewManager> initializedViews = new List<IViewManager>();

		public void InitializeViews()
		{
			initializedViews = new List<IViewManager>();

			loginView.Initialize();
			loginView.OnGoToRegister += GoToRegisterFromLogin;
			initializedViews.Add(loginView);

			registerView.Initialize();
			registerView.OnGoToLogin += GoToLoginFromRegister;
			initializedViews.Add(registerView);
		}

		private void OnDestroy()
		{
			loginView.OnGoToRegister -= GoToRegisterFromLogin;
			registerView.OnGoToLogin -= GoToLoginFromRegister;
		}

		public void SwapViews(IViewManager fromViewManager, IViewManager toViewManager)
		{
			fromViewManager.HideView();
			toViewManager.ShowView();
		}

		private void CloseAllViews()
		{
			foreach (IViewManager view in initializedViews)
			{
				view.HideView();
			}
		}

		public void ShowMainPage()
		{
			CloseAllViews();
			loginView.ShowView();
		}

		#region RoutingMethods
		private void GoToRegisterFromLogin() => SwapViews(loginView, registerView);
		private void GoToLoginFromRegister() => SwapViews(registerView, loginView);
		#endregion
	}
}