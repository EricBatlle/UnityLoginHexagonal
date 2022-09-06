namespace LoginHexagonal
{
	public class LocalizationBO : IBusinessObject
	{
		ILocalizationPort localizationPort;

		public LocalizationBO(ILocalizationPort localizationPort)
		{
			this.localizationPort = localizationPort;
		}

		public string GetLocalizedString(string stringKey)
		{
			return localizationPort.GetLocalizedString(stringKey);
		}
	}

}