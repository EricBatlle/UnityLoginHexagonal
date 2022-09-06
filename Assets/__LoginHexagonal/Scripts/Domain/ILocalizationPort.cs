namespace LoginHexagonal
{
	public interface ILocalizationPort
	{
		string GetLocalizedString(string tableName, string entryName);
	}
}