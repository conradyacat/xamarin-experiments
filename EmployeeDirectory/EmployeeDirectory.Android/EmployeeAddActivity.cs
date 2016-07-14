using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using Java.IO;
using CompatV7 = Android.Support.V7;

namespace EmployeeDirectory.Android
{
	[Activity(Label = "Add Employee", ParentActivity = typeof(MainActivity))]
	[MetaData(NavUtils.ParentActivity, Value = "EmployeeDirectory.Android.MainActivity")]
	public class EmployeeAddActivity : AppCompatActivity
	{
		private ImageButton _imageButton;
		private File _file;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.EmployeeAdd);

			var topToolbar = FindViewById<CompatV7.Widget.Toolbar>(Resource.Id.topToolbar);
			SetSupportActionBar(topToolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			if (AppContext.IsCameraAvailable)
			{
				_imageButton = FindViewById<ImageButton>(Resource.Id.photo);
				_imageButton.Click += (sender, e) =>
				{
					_file = new File(AppContext.PhotoDirectory, System.Guid.NewGuid() + ".jpg");
					var intent = new Intent(MediaStore.ActionImageCapture);
					intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));
					StartActivityForResult(intent, 0);
				};
			}

			var saveButton = FindViewById<Button>(Resource.Id.saveButton);
			saveButton.Click += (sender, e) =>
			{
				var hasError = false;
				var firstName = FindViewById<EditText>(Resource.Id.firstName);
				if (string.IsNullOrWhiteSpace(firstName.Text))
				{
					firstName.Error = "First Name is required";
					hasError = true;
				}

				var lastName = FindViewById<EditText>(Resource.Id.lastName);
				if (string.IsNullOrWhiteSpace(lastName.Text))
				{
					lastName.Error = "First Name is required";
					hasError = true;
				}

				var title = FindViewById<EditText>(Resource.Id.title);
				if (string.IsNullOrWhiteSpace(title.Text))
				{
					title.Error = "Last Name is required";
					hasError = true;
				}

				var officePhone = FindViewById<EditText>(Resource.Id.officePhone);
				if (string.IsNullOrWhiteSpace(officePhone.Text))
				{
					officePhone.Error = "Office Phone is required";
					hasError = true;
				}

				var mobilePhone = FindViewById<EditText>(Resource.Id.mobilePhone);
				if (string.IsNullOrWhiteSpace(mobilePhone.Text))
				{
					mobilePhone.Error = "Mobile Phone is required";
					hasError = true;
				}

				var email = FindViewById<EditText>(Resource.Id.email);
				if (string.IsNullOrWhiteSpace(email.Text))
				{
					email.Error = "Email is required";
					hasError = true;
				}

				if (hasError)
					return;

				var employee = new Employee
				{
					FirstName = firstName.Text,
					LastName = lastName.Text,
					Title = title.Text,
					OfficePhone = officePhone.Text,
					MobilePhone = mobilePhone.Text,
					Email = email.Text,
					PhotoFileName = (_file != null ? _file.Name : null)
				};

				DatabaseHelper.AddEmployee(employee);
				Snackbar.Make(FindViewById<View>(Resource.Id.empAddLayout), "New Employee Saved", Snackbar.LengthShort).Show();
				SetResult(Result.Ok, null);
				Finish();
			};
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.home:
					NavUtils.NavigateUpFromSameTask(this);
					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (resultCode == Result.Ok)
			{
				// Display in ImageView
				_imageButton.SetImageURI(Uri.FromFile(_file));
			}
		}

		protected override void OnDestroy()
		{
			System.GC.Collect();
			base.OnDestroy();
		}
	}
}

