using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using Java.IO;
using CompatV7 = Android.Support.V7;

namespace EmployeeDirectory.Android
{
	[Activity(Label = "Employee Details", ParentActivity = typeof(MainActivity))]
	[MetaData(NavUtils.ParentActivity, Value = "EmployeeDirectory.Android.MainActivity")]
    public class EmployeeDetailsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmployeeDetails);

			var topToolbar = FindViewById<CompatV7.Widget.Toolbar>(Resource.Id.topToolbar);
			SetSupportActionBar(topToolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            int employeeId = Intent.Extras.GetInt("EMPLOYEE_ID");

            Employee e = DatabaseHelper.GetEmployee(employeeId);
            var name = FindViewById<TextView>(Resource.Id.name);
            name.Text = e.FirstName + " " + e.LastName;

            var title = FindViewById<TextView>(Resource.Id.title);
            title.Text = e.Title;

            var officePhone = FindViewById<TextView>(Resource.Id.officePhoneNumber);
            officePhone.Text = e.OfficePhone;

            var mobilePhone = FindViewById<TextView>(Resource.Id.mobilePhoneNumber);
            mobilePhone.Text = e.MobilePhone;

            var email = FindViewById<TextView>(Resource.Id.email);
            email.Text = e.Email;

			if (!string.IsNullOrEmpty(e.PhotoFileName))
			{
				var photo = FindViewById<ImageView>(Resource.Id.photo);
				photo.SetImageURI(Uri.FromFile(new File(AppContext.PhotoDirectory, e.PhotoFileName)));
			}
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

		protected override void OnDestroy()
		{
			System.GC.Collect();
			base.OnDestroy();
		}
    }
}