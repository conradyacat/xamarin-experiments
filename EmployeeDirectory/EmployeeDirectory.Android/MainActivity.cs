using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using R = Android.Resource;

namespace EmployeeDirectory.Android
{
    [Activity(Label = "Employee Directory", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private string[] _employees = { "Christophe Coenraets", "John Smith" };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            DatabaseHelper.SeedData();

            Button searchButton = FindViewById<Button>(Resource.Id.searchButton);
            EditText searchKeyword = FindViewById<EditText>(Resource.Id.searchKeyword);
            ListView searchResult = FindViewById<ListView>(R.Id.List);

            searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));

            searchButton.Click += (sender, e) =>
            {
                searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));
            };
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var detailsIntent = new Intent(this, typeof(EmployeeDetailsActivity));
            var e = DatabaseHelper.GetEmployeeByPosition(position);
            detailsIntent.PutExtra("EMPLOYEE_ID", e.Id);
            StartActivity(detailsIntent);
        }
    }
}

