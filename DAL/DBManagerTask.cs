using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;

namespace HUS_project.DAL
{
    public class DBManagerTask
    {
        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerTask(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        /// <summary>
        /// Acquires all the bookings which have ended, but the Devices have not all been retrieved.
        /// </summary>
        /// <returns></returns>
        internal List<BookingModel> GetBookingRetrievalsToBeMade()
        {
            List<BookingModel> bookings = new List<BookingModel>();
            SqlConnection con = new SqlConnection(connectionString);

            // Acquire Bookings
            SqlCommand cmd = new SqlCommand("GetBookingsToBeRetrieved", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BookingModel booking = new BookingModel(
                    (int)reader["bookingID"],
                    (string)reader["orderedBy"],
                    new List<ItemLineModel>(),
                    new List<BookedDeviceModel>(),
                    new BuildingModel((string)reader["buildingName"], (byte)reader["roomNr"]),
                    (DateTime)reader["rentDate"],
                    (DateTime)reader["returnDate"],
                    1,
                    (string)reader["deliveredBy"],
                    (string)reader["bookingNotes"]);
                bookings.Add(booking);
            }
            con.Close();


            // Acquire ItemLines for Bookings ... Except these ItemLines only reflect how many of each Model this booking currently has out.
            cmd.CommandText = "GetCountUnreturnedModels";

            foreach (BookingModel booking in bookings)
            {
                cmd.Parameters.AddWithValue("@bookingID", booking.BookingID);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    booking.Items.Add(
                        new ItemLineModel(
                            (int)reader["UnreturnedDeiceModelsCount"],
                            new ModelModel(
                                (string)reader["modelName"],
                                "N/A - Irrelevant to purpose",
                                new CategoryModel((string)reader["categoryName"])
                                )
                            )
                        );
                }
                con.Close();
                cmd.Parameters.Clear();
            }
            return bookings;
        }

        internal List<BookingModel> GetBookingDeliveriesToBeMade ()
        {
            List<BookingModel> bookings = new List<BookingModel>();
            SqlConnection con = new SqlConnection(connectionString);

            // Acquire Bookings
            SqlCommand cmd = new SqlCommand("GetBookingsToBeDelivered", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BookingModel booking = new BookingModel(
                    (int)reader["bookingID"],
                    (string)reader["orderedBy"],
                    new List<ItemLineModel>(),
                    new List<BookedDeviceModel>(),
                    new BuildingModel((string)reader["buildingName"], (byte)reader["roomNr"]),
                    (DateTime)reader["rentDate"],
                    (DateTime)reader["returnDate"],
                    1,
                    "N/A",
                    (string)reader["bookingNotes"]);
                bookings.Add(booking);
            }
            con.Close();


            // Acquire ItemLines for Bookings
            cmd.CommandText = "GetBookingItemLines";

            foreach (BookingModel booking in bookings)
            {
                cmd.Parameters.AddWithValue("@bookingID", booking.BookingID);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    booking.Items.Add(
                        new ItemLineModel(
                            (int)reader["Quantity"],
                            new ModelModel(
                                (string)reader["modelName"],
                                "N/A - Irrelevant to the context",
                                new CategoryModel((string)reader["categoryName"])
                                )
                            )
                        );
                }
                con.Close();
                cmd.Parameters.Clear();
            }
            return bookings;
        }
    }
}
