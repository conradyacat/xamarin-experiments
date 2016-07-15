using Android.Graphics;
using Android.Media;

namespace EmployeeDirectory.Android.Util
{
	public static class BitmapHelpers
	{
		public static Bitmap LoadAndResizeBitmap(string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight ? outHeight / height : outWidth / width;
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

			return resizedBitmap;
		}

		public static Bitmap RotateToPortrait(string fileName)
		{
			var rotation = 0;

			using (var exif = new ExifInterface(fileName))
			{
				var orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Orientation.Normal);

				switch ((Orientation)orientation)
				{
					case Orientation.Rotate90:
						rotation = 90;
						break;
					case Orientation.Rotate180:
						rotation = 180;
						break;
					case Orientation.Rotate270:
						rotation = 270;
						break;
					default:
						rotation = 0;
						break;
				}
			}

			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = false };
			Bitmap bitmap = BitmapFactory.DecodeFile(fileName, options);

			if (rotation != 0)
			{
				int width = bitmap.Width;
				int height = bitmap.Height;
				using (var matrix = new Matrix())
				{
					matrix.PreRotate(rotation);
					Bitmap editedBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, width, height, matrix, false);
					return editedBitmap;
				}
			}

			return bitmap;
		}
	}
}

