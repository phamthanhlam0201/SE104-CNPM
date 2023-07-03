using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLCHVBDQ.DAO
{
    public class DataProvider
    {
        private static DataProvider instance; 

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider(); return DataProvider.instance;
            }

            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        private string connectionSTR = "Data Source=.\\SQLEXPRESS01;Initial Catalog=QLVBDQ;Integrated Security=True";

        
               

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }                    
                catch
                {
                    connection.Close();
                    return data;
                }
                connection.Close();
                return data;
            }
        }
        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {                
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    data = command.ExecuteNonQuery();
                }
                catch
                {
                    data = -1;
                }
                
                connection.Close();
                return data;
            }
        }
        public object ExecuteScalar(string query)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                data = command.ExecuteScalar();
                connection.Close();
                return data;
            }
        }
    }
}
