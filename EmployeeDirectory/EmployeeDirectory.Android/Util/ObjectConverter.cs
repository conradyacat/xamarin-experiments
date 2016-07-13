﻿namespace EmployeeDirectory.Android.Util
{
	public static class ObjectConverter
	{
		public static T Cast<T>(this Java.Lang.Object obj) where T : class
		{
			var propertyInfo = obj.GetType().GetProperty("Instance");
			return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
		}
	}
}

