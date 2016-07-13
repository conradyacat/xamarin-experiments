using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace EmployeeDirectory.Android.Database
{
    public static class DatabaseHelper
    {
		private static string DbPath;
		
        public static void Initialize()
        {
            using (var db = new SQLiteConnection(GetDbPath()))
            {
                db.CreateTable<Employee>();
            }
        }

        public static Employee GetEmployeeByPosition(int position)
        {
            var employees = GetEmployees("");
            return employees[position];
        }

        public static Employee GetEmployee(int id)
        {
            using (var db = new SQLiteConnection(GetDbPath()))
            {
                return db.Get<Employee>(id);
            }
        }

        public static IList<Employee> GetEmployees(string keyword)
        {
            using (var db = new SQLiteConnection(GetDbPath()))
            {
                return db.Query<Employee>("SELECT id, firstname, lastname, title FROM employee WHERE firstname || '' || lastname LIKE ?", "%" + keyword + "%");
            }
        }

		public static void AddEmployee(Employee employee)
		{
			using (var db = new SQLiteConnection(GetDbPath()))
			{
				db.Insert(employee);
			}
		}

		public static void DeleteEmployee(Employee employee)
		{
			using (var db = new SQLiteConnection(GetDbPath()))
			{
				db.Delete(employee);
			}
		}

        private static string GetDbPath()
        {
			return DbPath ?? (DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "employeedir.db3"));
        }
    }
}