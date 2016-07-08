
using Android.App;
using Android.OS;
using Android.Widget;
using EmployeeDirectory.Android.Database;

namespace EmployeeDirectory.Android
{
    [Activity(Label = "Employee Details")]
    public class EmployeeDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmployeeDetails);
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
        }
    }
}