using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoginHexagonal
{
	public class RegisterViewManager : MonoBehaviour, IViewManager
	{
		[SerializeField] private TMP_InputField usernameInputField = null;
		[SerializeField] private TMP_InputField passwordInputField = null;
		[Space()]
		[SerializeField] private TMP_Text errorMessage = null;
		[Space()]
		[SerializeField] private Button confirmButton = null;
		[SerializeField] private Button goToLoginButton = null;

		private RegisterBO registerBO;

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
		public event Action OnGoToLogin = null;

		public void Initialize()
		{
			confirmButton.onClick.AddListener(OnConfirmButton);
			goToLoginButton.onClick.AddListener(OnGoToLoginButton);

			registerBO = BusinessObjectLocator.GetBO<RegisterBO>();
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
				registerBO.Register(Username, Password);
				HideErrorMessage();
				OnLoginCompleted?.Invoke();
			}
			catch (FormatException)
			{
				DisplayErrorMessage("Password must have more than 6 characters");
			}
			catch (RegisterException)
			{
				DisplayErrorMessage("Impossible to Register");
			}
		}

		private void OnGoToLoginButton()
		{
			OnGoToLogin?.Invoke();
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
