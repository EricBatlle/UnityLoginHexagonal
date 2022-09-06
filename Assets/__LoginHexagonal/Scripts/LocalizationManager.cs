using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace LoginHexagonal
{
	public class LocalizationManager : MonoBehaviour
	{
		[SerializeField] private List<StringTable> localeStringTablesList;

		private List<StringTable> LocaleStringTablesList => localeStringTablesList;

		private IEnumerator Start()
		{
			yield return LocalizationSettings.InitializationOperation;
			LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;

			//GetAllLocaleTables
			Locale locale = LocalizationSettings.SelectedLocale;
			if (locale == null)
				throw new NullReferenceException();

			var getAllTablesOperation = LocalizationSettings.StringDatabase.GetAllTables(locale);
			yield return getAllTablesOperation;
			this.localeStringTablesList = getAllTablesOperation.Result.ToList();

			GetString();
		}

		private void OnDestroy()
		{
			LocalizationSettings.SelectedLocaleChanged -= SelectedLocaleChanged;
		}

		private void SelectedLocaleChanged(Locale locale)
		{
			StartCoroutine(GetAllLocaleTables(locale));
		}

		private IEnumerator GetAllLocaleTables(Locale locale)
		{
			var getAllTablesOperation = LocalizationSettings.StringDatabase.GetAllTables(locale);
			yield return getAllTablesOperation;
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

		private string GetLocalizedString(string entryName)
		{
			foreach (StringTable stringTable in LocaleStringTablesList)
			{
				StringTableEntry entry = stringTable.GetEntry(entryName);
				if (entry != null)
				{
					return entry.GetLocalizedString();
				}
			}

			throw new KeyNotFoundException();
		}

		public void GetString()
		{
			Debug.Log(GetLocalizedString(localeStringTablesList[0], "Login"));
			Debug.Log(GetLocalizedString("Login2"));
		}

	}
}