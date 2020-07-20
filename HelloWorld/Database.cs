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
        static public SqlConnection Connect()
        {
            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Data Source=(local);Initial Catalog=FuelPumpDB;Integrated Security=SSPI");
                thisConnection.Open();
                return thisConnection;
            }
            catch
            {
                MessageBox.Show("Database Connection Error");
                return null;
            }

        }

        static public void creditorinsert(string name, string amount)
        {
            SqlCommand cmd;
            SqlDataAdapter adapter;
            string query;
            query = "insert into creditorData (creditorName,amount) values ('"+name+"','"+amount+"')";
            adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, Connect());
            adapter.InsertCommand.ExecuteNonQuery();
        }

        static public Dictionary<string,string> creditorGetData()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            SqlCommand cmd;
            SqlDataReader reader;
            string query = "Select * from creditorData";
            cmd = new SqlCommand(query, Connect());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                d.Add(reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
            }
            return d;

        }

        static public void creditorUpdate(string name,string amount)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            string query = "Update creditorData set amount = '"+amount+"' where creditorName = '"+name+"'";
            adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(query, Connect());
            adapter.UpdateCommand.ExecuteNonQuery();
        }

        public void database()
        {


            try
            {
                ////SqlConnection thisConnection = new SqlConnection(@"Server=(local);Database=Test;Trusted_Connection=Yes;");
                SqlConnection thisConnection = new SqlConnection(@"Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI");
                thisConnection.Open();

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


                SqlCommand cmd;
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
