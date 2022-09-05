using System;
using System.Collections.Generic;

namespace LoginHexagonal
{
	public static class BusinessObjectLocator
	{
		private static readonly Dictionary<Type, IBusinessObject> dict = new Dictionary<Type, IBusinessObject>();

		public static T GetBO<T>() where T : IBusinessObject
		{
			return (T)dict[typeof(T)];
		}

		public static void AddBO(IBusinessObject businessObject)
		{
			dict.Add(businessObject.GetType(), businessObject);
		}
	}
}