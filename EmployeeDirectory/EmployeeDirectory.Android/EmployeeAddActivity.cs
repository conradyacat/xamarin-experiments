
using Android.App;
using Android.OS;
using Android.Widget;
using EmployeeDirectory.Android.Database;

namespace EmployeeDirectory.Android
{
	[Activity(Label = "Add Employee")]
	public class EmployeeAddActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.EmployeeAdd);

			var saveButton = FindViewById<Button>(Resource.Id.saveButton);
			saveButton.Click += (sender, e) =>
			{
				var firstName = FindViewById<EditText>(Resource.Id.firstName);
				var lastName = FindViewById<EditText>(Resource.Id.lastName);
				var title = FindViewById<EditText>(Resource.Id.title);
				var officePhone = FindViewById<EditText>(Resource.Id.officePhone);
				var mobilePhone = FindViewById<EditText>(Resource.Id.mobilePhone);
				var email = FindViewById<EditText>(Resource.Id.email);

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
	}
}

