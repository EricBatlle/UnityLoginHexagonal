using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoginHexagonal
{
	public class LoginViewManager : MonoBehaviour, IViewManager
	{
		[SerializeField] private TMP_InputField usernameInputField = null;
		[SerializeField] private TMP_InputField passwordInputField = null;
		[Space()]
		[SerializeField] private TMP_Text errorMessage = null;
		[Space()]
		[SerializeField] private Button confirmButton = null;
		[SerializeField] private Button goToRegisterButton = null;

		private LoginBO loginBO;
		private LocalizationBO localizationBO;

		private string Username
		{
			get => usernameInputField.text;
			set => usernameInputField.text = value;
		}
		private string Password
		{
			get => passwordInputField.text;
			set => passwordInputField.text = value;
		}

		public event Action OnLoginCompleted = null;
		public event Action OnGoToRegister = null;

		public void Initialize()
		{
			confirmButton.onClick.AddListener(OnConfirmButton);
			goToRegisterButton.onClick.AddListener(OnGoToRegisterButton);

			loginBO = BusinessObjectLocator.GetBO<LoginBO>();
			localizationBO = BusinessObjectLocator.GetBO<LocalizationBO>();
		}

		public void ShowView()
		{
			this.gameObject.SetActive(true);
			HideErrorMessage();
		}

		public void HideView()
		{
			this.gameObject.SetActive(false);
		}

		private void OnConfirmButton()
		{
			try
			{
				loginBO.Login(Username, Password);
				HideErrorMessage();
				OnLoginCompleted?.Invoke();
			}
			catch (FormatException)
			{
				DisplayErrorMessage(localizationBO.GetLocalizedErrorMessage("ShortPassword"));
			}
			catch (LoginException)
			{
				DisplayErrorMessage(localizationBO.GetLocalizedErrorMessage("ImpossibleToLogin"));
			}
		}

		private void OnGoToRegisterButton()
		{
			OnGoToRegister?.Invoke();
		}

		private void DisplayErrorMessage(string message = "Error")
		{
			errorMessage.gameObject.SetActive(true);
			errorMessage.text = message;
		}
		private void HideErrorMessage()
		{
			errorMessage.gameObject.SetActive(false);
			errorMessage.text = string.Empty;
		}
	}
}
