using Android.OS;
using Java.IO;

namespace EmployeeDirectory.Android
{
	public static class AppContext
	{
		static AppContext()
		{
			PhotoDirectory = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "EmployeeDir");
		}

		public static File PhotoDirectory { get; private set; }

		public static bool IsCameraAvailable { get; set; }
	}
}

