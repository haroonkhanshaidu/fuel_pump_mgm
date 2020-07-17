using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class Database
    {
        static public SqlConnection connect()
        {
            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI");
                thisConnection.Open();
                return thisConnection;
            }
            catch
            {

            }

        }

        public void database()
        {


            try
            {
                ////SqlConnection thisConnection = new SqlConnection(@"Server=(local);Database=Test;Trusted_Connection=Yes;");
                SqlConnection thisConnection = new SqlConnection(@"Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI");
                thisConnection.Open();
                SqlCommand cmd;

                SqlDataAdapter adapter;
                string query;
                query = "insert into [Test].[dbo].[test] (Name,id) values ('Nam','id')";
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(query, thisConnection);
                //adapter.InsertCommand.ExecuteNonQuery();

                query = "Update [Test].[dbo].[test] set Name = 'Waqar A' where id = '1'";
                adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(query, thisConnection);
                adapter.UpdateCommand.ExecuteNonQuery();

                query = "Delete [Test].[dbo].[test] where id = '1'";
                adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(query, thisConnection);
                adapter.UpdateCommand.ExecuteNonQuery();


                SqlDataReader reader;
                query = "Select * from dbo.test";
                cmd = new SqlCommand(query, thisConnection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show(reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString());
                }

            }
            catch
            {
                MessageBox.Show("db error");
            }
        }

    }
}
