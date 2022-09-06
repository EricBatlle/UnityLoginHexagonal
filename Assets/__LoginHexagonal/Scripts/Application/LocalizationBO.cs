namespace LoginHexagonal
{
	public class LocalizationBO : IBusinessObject
	{
		ILocalizationPort localizationPort;

		public LocalizationBO(ILocalizationPort localizationPort)
		{
			this.localizationPort = localizationPort;
		}

		public string GetLocalizedString(string tableName, string stringKey)
		{
			return localizationPort.GetLocalizedString(tableName, stringKey);
		}

		public string GetLocalizedErrorMessage(string stringKey)
		{
			return localizationPort.GetLocalizedString("ErrorMessage", stringKey);
		}
	}

}