using SQLite;

namespace EmployeeDirectory.Android
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string OfficePhone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

		public string PhotoFileName { get; set; }
    }
}