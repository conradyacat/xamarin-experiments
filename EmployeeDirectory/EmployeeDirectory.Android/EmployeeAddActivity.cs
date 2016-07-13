
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
	}
}

