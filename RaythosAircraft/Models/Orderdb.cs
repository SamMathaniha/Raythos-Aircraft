using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RaythosAircraft.Models;

namespace RaythosAircraft.Models
{
    public class Orderdb
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-L7T2V25//SQLEXPRESS;Initial Catalog=RaythosAircraft;Integrated Security=True");

        public string Severecode(Order ord)
        {
            try
            {
                SqlCommand com = new SqlCommand("Order", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", ord.Name);
                com.Parameters.AddWithValue("@Total", ord.Total);
                com.Parameters.AddWithValue("@PurchaseDate", ord.PurchaseDate);
                com.Parameters.AddWithValue("@DeliveryDate", ord.DeliveryDate);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return "OK"; // Return a success message
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message; // Return the error message
            }
        }
    }
}
