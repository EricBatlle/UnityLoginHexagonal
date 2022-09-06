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
		private Dictionary<string, StringTable> localeStringTablesDict = new Dictionary<string, StringTable>();

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

			await GetAllLocaleTables(locale);
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
			localeStringTablesDict.Clear();

			var getAllTablesOperation = LocalizationSettings.StringDatabase.GetAllTables(locale);
			await getAllTablesOperation.Task;
			getAllTablesOperation.Result.ToList().ForEach(stringTable => localeStringTablesDict.Add(stringTable.TableCollectionName, stringTable));
		}

		private static string GetLocalizedString(StringTable table, string entryName)
		{
			// Get the table entry. The entry contains the localized string and Metadata
			var entry = table.GetEntry(entryName);

			if (entry == null)
				throw new KeyNotFoundException();

			return entry.GetLocalizedString(); // We can pass in optional arguments for Smart Format or String.Format here.
		}
		
		public string GetLocalizedString(string tableName, string entryName)
		{
			if(localeStringTablesDict.TryGetValue(tableName, out StringTable stringTable))
			{
				return GetLocalizedString(stringTable, entryName);
			}
			else
			{
				throw new KeyNotFoundException();
			}
		}
		
	}

}