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
            booking.BookingID = bookingID;
            booking.Devices = new List<DeviceModel>();
            booking.Items = new List<ItemLineModel>();
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetBookingOnID", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                booking.Customer = (string)reader["orderedBy"];
                booking.BookingStatus = (byte)reader["status"];
                booking.DeliveredBy = (string)reader["deliveredBy"];
                booking.Notes = (string)reader["bookingNotes"];
                booking.PlannedBorrowDate = (DateTime)reader["rentDate"];
                booking.PlannedReturnDate = (DateTime)reader["returnDate"];
                booking.Location = new BuildingModel(
                    (string)reader["buildingName"],
                    (byte)reader["roomNr"]
                    );
            }
            con.Close();

            return booking;
        }

    }
}
