using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSchool
{
    public class StudentSession
    {

        public static int Id { get; private set; }
        public static string ClassNumber { get; set; }


        public static bool SetStudent(int id)
        {
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Класс] FROM SchoolStudents WHERE [Идентификатор] = " + id + "", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Id = id;
                ClassNumber = reader.GetString(0);
                connection.Close();
                return true;
            }

            return false;
        }
    }
}
