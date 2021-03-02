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

        internal List<BookingModel> GetBookingRetrievalsToBeMade()
        {
            List<BookingModel> bookings = new List<BookingModel>();
            bool ready = false;

            if (ready)
            {



                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("GetFinishedBookings", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Acquire Bookings
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    BookingModel booking = new BookingModel(
                        (int)reader["b.bookingID"],
                        (string)reader["b.orderedBy"],
                        new List<ItemLineModel>(),
                        new List<DeviceModel>(),
                        new BuildingModel((string)reader["r.buildingName"], (byte)reader["r.roomNr"]),
                        (DateTime)reader["rentDate"],
                        (DateTime)reader["returnDate"],
                        1,
                        (string)reader["b.deliveredBy"]);
                    bookings.Add(booking);
                }

                // Acquire ItemLines for Booking
                cmd.CommandText = "GetBookingItemLines";

                foreach (BookingModel booking in bookings)
                {
                    cmd.Parameters.AddWithValue("@bookingID", booking.BookingID);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        booking.Items.Add(
                            new ItemLineModel(
                                (int)reader["ItemLine.Quantity"],
                                new ModelModel(
                                    (string)reader["Model.modelName"],
                                    "N/A",
                                    new CategoryModel((string)reader["Category.categoryName"])
                                    )
                                )
                            );
                    }
                    cmd.Parameters.Clear();
                }

                con.Close();

            }
            return bookings;
        }

        internal List<BookingModel> GetBookingDeliveriesToBeMade ()
        {
            List<BookingModel> bookings = new List<BookingModel>();

            return bookings;
        }
    }
}
