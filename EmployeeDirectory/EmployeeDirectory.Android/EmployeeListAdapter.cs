using System.Collections.Generic;
using Android.App;
using Android.Net;
using Android.Views;
using Android.Widget;
using Java.IO;
using Square.Picasso;

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
                view = _context.LayoutInflater.Inflate(Resource.Layout.EmployeeListItem, null);

			view.FindViewById<TextView>(Resource.Id.name).Text = e.FirstName + " " + e.LastName;
            view.FindViewById<TextView>(Resource.Id.title).Text = e.Title;
			if (!string.IsNullOrEmpty(e.PhotoFileName))
			{
				Picasso.With(_context)
					   .Load(Uri.FromFile(new File(AppContext.PhotoDirectory, e.PhotoFileName)))
					   .Resize(50, 50)
					   .CenterCrop()
					   .Tag(_context)
					   .Into(view.FindViewById<ImageView>(Resource.Id.photo));
			}

            return view;
        }
    }
}