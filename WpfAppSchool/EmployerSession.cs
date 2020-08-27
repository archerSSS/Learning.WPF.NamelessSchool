using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSchool
{
    public class EmployerSession
    {
        private const byte flag_principal = 3;
        private const byte flag_teacher = 2;
        public static int Id { get; private set; }
        private static string ClassNumber { get; set; }
        private static byte Credentials { get; set; }

        public static bool IsPrincipal()
        {
            return flag_principal == Credentials;
        }

        public static bool SetEmployer(int id)
        {
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Класс], [Должность] FROM SchoolEmployees WHERE [Идентификатор] = " + id + "", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Id = id;
                bool b = reader.IsDBNull(0);
                if (!reader.IsDBNull(0)) ClassNumber = reader.GetString(0);
                string auth = reader.GetString(1);
                if (auth == "Преподаватель") Credentials = 2;
                else if (auth == "Директор") Credentials = 3;
                else Credentials = 1;

                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }

        public static string GetClassNumber()
        {
            if (ClassNumber == null) return ""; 
            return ClassNumber;
        }
    }
}
