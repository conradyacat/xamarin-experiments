using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using CompatToolbar = Android.Support.V7.Widget.Toolbar;
using R = Android.Resource;

namespace EmployeeDirectory.Android
{
    [Activity(Label = "Employee Directory", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            DatabaseHelper.SeedData();

			CompatToolbar topToolbar = FindViewById<CompatToolbar>(Resource.Id.topToolbar);
			SetSupportActionBar(topToolbar);

            Button searchButton = FindViewById<Button>(Resource.Id.searchButton);
            EditText searchKeyword = FindViewById<EditText>(Resource.Id.searchKeyword);
            ListView searchResult = FindViewById<ListView>(R.Id.List);

            searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));
			searchResult.ItemClick += (sender, e) =>
			{
				var detailsIntent = new Intent(this, typeof(EmployeeDetailsActivity));
				var employee = DatabaseHelper.GetEmployeeByPosition(e.Position);
				detailsIntent.PutExtra("EMPLOYEE_ID", employee.Id);
				StartActivity(detailsIntent);
			};
			searchResult.ItemLongClick += (sender, e) =>
			{
				
			};

            searchButton.Click += (sender, e) =>
            {
                searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(searchKeyword.Text));
            };
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.home, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.menu_add_emp:
					var addIntent = new Intent(this, typeof(EmployeeAddActivity));
					StartActivityForResult(addIntent, RequestCode.ADD_EMPLOYEE);
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
				switch (requestCode)
				{
					case RequestCode.ADD_EMPLOYEE:
						ListView searchResult = FindViewById<ListView>(R.Id.List);
						searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(""));
						break;
				}
			}
		}
    }
}

