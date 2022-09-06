using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace LoginHexagonal
{
	public class UnityLocalizationAdapter : ILocalizationPort
	{
		private List<StringTable> localeStringTablesList;

		private UnityLocalizationAdapter() { }

		public static async Task<UnityLocalizationAdapter> CreateUnityLocalizationAdapterAsync()
		{
			UnityLocalizationAdapter instance = new UnityLocalizationAdapter();
			await instance.InitializeAsync();
			return instance;
		}

		private async Task InitializeAsync()
		{
			await LocalizationSettings.InitializationOperation.Task;
			LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;

			//GetAllLocaleTables
			Locale locale = LocalizationSettings.SelectedLocale;
			if (locale == null)
				throw new NullReferenceException();

			var getAllTablesOperation = LocalizationSettings.StringDatabase.GetAllTables(locale);
			await getAllTablesOperation.Task;
			this.localeStringTablesList = getAllTablesOperation.Result.ToList();
		}

		~UnityLocalizationAdapter()
		{
			LocalizationSettings.SelectedLocaleChanged -= SelectedLocaleChanged;
		}

		private async void SelectedLocaleChanged(Locale locale)
		{
			await GetAllLocaleTables(locale);
		}

		private async Task GetAllLocaleTables(Locale locale)
		{
			var getAllTablesOperation = LocalizationSettings.StringDatabase.GetAllTables(locale);
			await getAllTablesOperation.Task;
			this.localeStringTablesList = getAllTablesOperation.Result.ToList();
		}

		private static string GetLocalizedString(StringTable table, string entryName)
		{
			// Get the table entry. The entry contains the localized string and Metadata
			var entry = table.GetEntry(entryName);

			if (entry == null)
				throw new KeyNotFoundException();

			return entry.GetLocalizedString(); // We can pass in optional arguments for Smart Format or String.Format here.
		}

		public string GetLocalizedString(string entryName)
		{
			foreach (StringTable stringTable in localeStringTablesList)
			{
				StringTableEntry entry = stringTable.GetEntry(entryName);
				if (entry != null)
				{
					return entry.GetLocalizedString();
				}
			}

			throw new KeyNotFoundException();
		}
	}

}