using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Support.V7.App;
using Android.Util;
using EmployeeDirectory.Android.Database;
using Java.IO;

namespace EmployeeDirectory.Android
{
	public static class Bootstrapper
	{
		public static void Bootstrap(AppCompatActivity activity)
		{
			DatabaseHelper.Initialize();

			AppContext.IsCameraAvailable = IsThereAnAppToTakePictures(activity);
			if (AppContext.IsCameraAvailable)
				CreateDirectoryForPictures();
		}

		private static void CreateDirectoryForPictures()
		{
			var photoDir = AppContext.PhotoDirectory;

			if (!photoDir.Exists())
			{
				Log.Verbose(nameof(Bootstrapper), "Creating dir for pictures");
				photoDir.Mkdirs();
			}
		}

		private static bool IsThereAnAppToTakePictures(AppCompatActivity activity)
		{
			var intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				activity.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}
	}
}

