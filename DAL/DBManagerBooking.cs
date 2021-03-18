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
        
        /// <summary>
        /// Gets all Booking information based on the bookingID provided.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all the ItemLines for the bookingID provided.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        internal List<ItemLineModel> GetItemLines(int bookingID)
        {
            List<ItemLineModel> itemLines = new List<ItemLineModel>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetBookingItemLines", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                itemLines.Add(
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

            return itemLines;
        }

        internal List<DeviceModel> GetBookedDevices(int bookingID)
        {
            List<DeviceModel> bookedDevices = new List<DeviceModel>();



            return bookedDevices;
        }

        /// <summary>
        /// Acquires the location of a random Device of the modelName which is currently in storage.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        internal StorageLocationModel GetModelLocation(string modelName)
        {
            StorageLocationModel location = new StorageLocationModel();
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetAStorageLocationForModel", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@modelName", modelName);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                location.ShelfName = (string)reader["shelfName"];
                location.ShelfLevel = (byte)reader["shelfLevel"];
                location.ShelfSpot = (byte)reader["shelfSpot"];
                location.Location = new BuildingModel(
                        (string)reader["buildingName"],
                        (byte)reader["roomNr"]
                    );
            }
            con.Close();

            return location;
        }
    }
}
