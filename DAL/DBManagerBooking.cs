using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;

namespace HUS_project.DAL
{
    public class DBManagerBooking
    {
        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerBooking(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        internal void CreateBooking()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);

            con.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Close();

        }

        internal BookingModel GetBooking(int bookingID)
        {
            BookingModel booking = new BookingModel();
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetBookingInfo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                booking.BookingID = bookingID;
                booking.Customer = reader["orderedBy"].ToString();
            }
            con.Close();

            return booking;
        }

    }
}
