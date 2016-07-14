using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Android.Database;
using EmployeeDirectory.Android.Util;
using CompatV7 = Android.Support.V7;
using R = Android.Resource;

namespace EmployeeDirectory.Android
{
	[Activity(Label = "Employee Directory", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTop)]
	public class MainActivity : AppCompatActivity, CompatV7.Widget.SearchView.IOnQueryTextListener
    {
		private ListView _searchResult;
		private CompatV7.Widget.SearchView _searchView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var topToolbar = FindViewById<CompatV7.Widget.Toolbar>(Resource.Id.topToolbar);
			SetSupportActionBar(topToolbar);

			Bootstrapper.Bootstrap(this);

			var refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
			refresher.Refresh += async (sender, e) =>
			{
				var employees = await Task.Run(() =>
				{
					refresher.Refreshing = true;
					return DatabaseHelper.GetEmployees("");
				});
				_searchResult.Adapter = new EmployeeListAdapter(this, employees);
				refresher.Refreshing = false;
			};

			var addEmpFab = FindViewById<FloatingActionButton>(Resource.Id.addEmpButton);
			addEmpFab.Click += (sender, e) =>
			{
				var addIntent = new Intent(this, typeof(EmployeeAddActivity));
				addIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				StartActivityForResult(addIntent, RequestCode.ADD_EMPLOYEE);
			};

            _searchResult = FindViewById<ListView>(R.Id.List);
            _searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(""));
			_searchResult.ItemClick += (sender, e) =>
			{
				var detailsIntent = new Intent(this, typeof(EmployeeDetailsActivity));
				detailsIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				var employee = e.Parent.GetItemAtPosition(e.Position).Cast<Employee>();
				detailsIntent.PutExtra("EMPLOYEE_ID", employee.Id);
				StartActivity(detailsIntent);
			};

			_searchResult.ItemLongClick += (sender, e) =>
			{
				var employee = e.Parent.GetItemAtPosition(e.Position).Cast<Employee>();
				string name = employee.FirstName + " " + employee.LastName;
				Dialog confirmDialog = new CompatV7.App.AlertDialog.Builder(this)
										.SetTitle("Confirm Delete")
										.SetMessage("Do you want to delete \"" + name + "\"?")
										.SetPositiveButton("Delete", (senderAlert, eAlert) =>
											{
												DatabaseHelper.DeleteEmployee(employee);
												Snackbar.Make(FindViewById<View>(Resource.Id.mainLayout), "\"" + name + "\" deleted", Snackbar.LengthShort).Show();
												_searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(""));
											})
	                                   .SetNegativeButton("Cancel", (senderAlert, eAlert) => { })
	                                   .Create();
				confirmDialog.Show();
			};
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.home, menu);
			IMenuItem searchItem = menu.FindItem(Resource.Id.menu_search);
			View view = MenuItemCompat.GetActionView(searchItem);
			_searchView = view.JavaCast<CompatV7.Widget.SearchView>();
			_searchView.SetOnQueryTextListener(this);
			return base.OnCreateOptionsMenu(menu);
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

		public bool OnQueryTextChange(string newText)
		{
			if (string.IsNullOrWhiteSpace(newText))
			{
				_searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(newText));
				return true;
			}

			return false;
		}

		public bool OnQueryTextSubmit(string query)
		{
			_searchResult.Adapter = new EmployeeListAdapter(this, DatabaseHelper.GetEmployees(query));
			return true;
		}
	}
}

