using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using CompatV7 = Android.Support.V7;

namespace EmployeeDirectory.Android
{
	[Activity(Label = "Add Employee", ParentActivity = typeof(MainActivity))]
	[MetaData("android.support.PARENT_ACTIVITY", Value = "EmployeeDirectory.Android.MainActivity")]
	public class EmployeeAddActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.EmployeeAdd);

			var topToolbar = FindViewById<CompatV7.Widget.Toolbar>(Resource.Id.topToolbar);
			SetSupportActionBar(topToolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

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
					Email = email.Text
				};

				DatabaseHelper.AddEmployee(employee);
				Toast.MakeText(this, "New Employee Saved", ToastLength.Short).Show();
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
	}
}

