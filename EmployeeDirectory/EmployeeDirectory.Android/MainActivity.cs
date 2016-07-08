using Android.App;
using Android.OS;
using Android.Widget;
using EmployeeDirectory.Android.Database;

namespace EmployeeDirectory.Android
{
    [Activity(Label = "Employee Directory", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
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
            ListView searchResult = FindViewById<ListView>(Resource.Id.searchResult);

            searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));

            searchButton.Click += (sender, e) =>
            {
                searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));
            };
        }
    }
}

