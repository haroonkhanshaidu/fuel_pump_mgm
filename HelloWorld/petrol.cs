using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class petrol
    {
       
        static public SqlConnection Connect()
        {

            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Data Source=DESKTOP-792H4GJ\SQLEXPRESS;Initial Catalog=FuelPumpDB;Integrated Security=True"); thisConnection.Open();
                return thisConnection;
            }
            catch
            {
                MessageBox.Show("Database Connection Error");
                return null;
            }

        }

        public void Validate(object sender, EventArgs e, ArrayList list)
        {

        }

    }
}
