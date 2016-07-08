using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace EmployeeDirectory.Android
{
    public class EmployeeListAdapter : BaseAdapter<Employee>
    {
        private readonly IList<Employee> _employees;
        private readonly Activity _context;

        public EmployeeListAdapter(Activity context, IList<Employee> employees)
        {
            _context = context;
            _employees = employees;
        }

        public override Employee this[int position]
        {
            get
            {
                return _employees[position];
            }
        }

        public override int Count
        {
            get
            {
                return _employees.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var e = _employees[position];
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.employee_list_item, null);

            view.FindViewById<TextView>(Resource.Id.firstName).Text = e.FirstName;
            view.FindViewById<TextView>(Resource.Id.lastName).Text = e.LastName;
            view.FindViewById<TextView>(Resource.Id.title).Text = e.Title;

            return view;
        }
    }
}