using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace EmployeeDirectory.Android.Database
{
    public static class DatabaseHelper
    {
        public static void SeedData()
        {
            using (var db = new SQLiteConnection(GetDbPath()))
            {
                db.CreateTable<Employee>();
                db.DeleteAll<Employee>();

                var employee = new Employee
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Title = "CEO",
                    OfficePhone = "617-219-2001",
                    MobilePhone = "617-456-7890",
                    Email = "jsmith@email.com"
                };
                db.Insert(employee);

                employee = new Employee
                {
                    FirstName = "Robert",
                    LastName = "Jackson",
                    Title = "VP Engineering",
                    OfficePhone = "617-219-3333",
                    MobilePhone = "781-444-2222",
                    Email = "rjackson@email.com"
                };
                db.Insert(employee);

                employee = new Employee
                {
                    FirstName = "Marie",
                    LastName = "Potter",
                    Title = "VP Sales",
                    OfficePhone = "617-219-2002",
                    MobilePhone = "987-654-3210",
                    Email = "mpotter@email.com"
                };
                db.Insert(employee);
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

        private static string GetDbPath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "employeedir.db3");
        }
    }
}