using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;
using System.Data;

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
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                booking.Customer = (string)reader["orderedBy"];
                // DeliveredBy and Notes may be DBNull (Not the same as null for C#), which means pulling them out and converting to string
                // - gives a critical error.
                booking.DeliveredBy = reader["deliveredBy"] == DBNull.Value ? null : (string)reader["deliveredBy"];
                booking.Notes = reader["bookingNotes"] == DBNull.Value ? "" : (string)reader["bookingNotes"];
                booking.PlannedBorrowDate = (DateTime)reader["rentDate"];
                booking.PlannedReturnDate = (DateTime)reader["returnDate"];
                booking.Location = new BuildingModel(
                    (string)reader["buildingName"],
                    (string)reader["roomNr"]
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
            cmd.CommandType = CommandType.StoredProcedure;
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

        /// <summary>
        /// Acquires all the bookedDevices for a booking.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        internal List<DeviceModel> GetBookedDevices(int bookingID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetBookedDevicesForBooking", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            List<DeviceModel> bookedDevices = new List<DeviceModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                bookedDevices.Add(
                    new DeviceModel(
                        (int)reader["deviceID"],
                        (string)reader["serialNumber"],
                        new ModelModel(
                            (string)reader["modelName"],
                            "Model Description: Irrelevant to purpose",
                            new CategoryModel(
                                (string)reader["categoryName"]
                            )),
                        new StorageLocationModel(),
                        1,
                        "Device Note: Irrelevant to purpose",
                        DateTime.Now,
                        "Latest change to Device by: Irrelevant to purpose",
                        reader["returnedBy"] == DBNull.Value ? null : (string)reader["returnedBy"],
                        reader["returnedDate"] == DBNull.Value ? DateTime.Now : (DateTime)reader["returnedDate"]
                        ));
            }
            con.Close();

            return bookedDevices;
        }

        internal List<BookingModel> GetUserBookingsCurrent(string uniLogin)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetUserBookingsCurrent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uniLogin", uniLogin);

            List<BookingModel> bookings = new List<BookingModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string[] room = reader["Room"].ToString().Split('.');
                bookings.Add(
                    new BookingModel(
                        (int)reader["bookingID"],
                        uniLogin,
                        new List<ItemLineModel>(),
                        new List<DeviceModel>(),
                        new BuildingModel(room[0], room[1]),
                        (DateTime)reader["rentDate"],
                        (DateTime)reader["returnDate"],
                        reader["deliveredBy"].ToString(),
                        reader["bookingNotes"].ToString()
                        ));
            }
            con.Close();
            return bookings;
        }

        internal List<BookingModel> GetUserBookingsOpen(string uniLogin)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetUserBookingsOpen", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uniLogin", uniLogin);

            List<BookingModel> bookings = new List<BookingModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string[] room = reader["Room"].ToString().Split('.');
                bookings.Add(
                    new BookingModel(
                        (int)reader["bookingID"],
                        uniLogin,
                        new List<ItemLineModel>(),
                        new List<DeviceModel>(),
                        new BuildingModel(room[0], room[1]),
                        (DateTime)reader["rentDate"],
                        (DateTime)reader["returnDate"],
                        null,
                        reader["bookingNotes"].ToString()
                        ));
            }
            con.Close();
            return bookings;
        }

        /// <summary>
        /// Acquires the User's latest BookingLogs for each unique Booking which is done.
        /// </summary>
        /// <param name="uniLogin"></param>
        /// <returns></returns>
        internal List<BookingModel> GetUserBookingsOld(string uniLogin)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetUserBookingsClosed", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uniLogin", uniLogin);

            List<BookingModel> bookings = new List<BookingModel>();
            List<int> bookingLogIDs = new List<int>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() && bookings.Count() < 25)
            {
                string[] room = reader["room"].ToString().Split('.');
                bookingLogIDs.Add((int)reader["bookingLogID"]);
                bookings.Add(
                    new BookingModel(
                        (int)reader["bookingID"],
                        uniLogin,
                        new List<ItemLineModel>(),
                        new List<DeviceModel>(),
                        new BuildingModel(room[0], room[1]),
                        (DateTime)reader["rentDate"],
                        (DateTime)reader["returnDate"],
                        reader["deliveredBy"].ToString(),
                        reader["bookingNotes"].ToString()
                        ));
            }
            con.Close();

            // Here we get the ItemLineLogs for the booking
            for (int i = 0; i < bookings.Count; i++)
            {
                bookings[i].Items = GetBookingLogItemLineLogs(bookingLogIDs[i]);
            }

            return bookings;
        }

        /// <summary>
        /// Acquires all the ItemLineLogs for the given BookingLogID, if any.
        /// </summary>
        /// <param name="bookingLogID"></param>
        /// <returns></returns>
        internal List<ItemLineModel> GetBookingLogItemLineLogs(int bookingLogID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetBookingLogItemLineLogs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingLogID", bookingLogID);

            List<ItemLineModel> items = new List<ItemLineModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(
                    new ItemLineModel(
                        (int)reader["quantity"],
                        new ModelModel(
                            reader["modelName"].ToString(),
                            "",
                            new CategoryModel(
                                reader["categoryName"].ToString()
                                )
                            )
                        )
                    );
            }
            con.Close();

            return items;
        }


        /// <summary>
        /// Updates BookedDevice to be Returned
        /// </summary>
        /// <param name="deviceID">DeviceID of the BookedDevice</param>
        /// <param name="bookingID">BookingID of the BookedDevice</param>
        /// <param name="returnedBy">Who is logged in, marked this as returned</param>
        internal void ReturnBookedDevice(int deviceID, int bookingID, string returnedBy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("ReturnBookedDevice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deviceID", deviceID);
            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            cmd.Parameters.AddWithValue("@returnedBy", returnedBy);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Creates BookedDevice, if the device is available.
        /// </summary>
        /// <param name="deviceID">DeviceID of the BookedDevice</param>
        /// <param name="bookingID">BookingID of the BookedDevice</param>
        /// <returns>True if Successful</returns>
        internal bool CreateBookedDevice(int deviceID, int bookingID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("CreateBookedDevice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deviceID", deviceID);
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            con.Open();
            int output = (int)cmd.ExecuteScalar();
            bool success = Convert.ToBoolean(output);
            con.Close();
            return success;
        }

        /// <summary>
        /// Creates BookedDeviceLogs for each BookedDevice for a Booking. .. And Sets the Devices to StorageLocation 3 (Udlånt)
        /// </summary>
        /// <param name="bookingLogID"></param>
        /// <param name="bookingID"></param>
        internal void CreateBookedDevicesLogs(int bookingLogID, int bookingID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("CreateBookedDevicesLogs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingLogID", bookingLogID);
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



        /// <summary>
        /// Updates the Booking and Logs the changes.
        /// </summary>
        /// <param name="updatedBooking"></param>
        /// <param name="alterer">Who's logged in, making these changes</param>
        /// <returns>bookingLogID, for ItemLineLogs</returns>
        internal int UpdateBookingAndLog(BookingModel updatedBooking, string alterer, string deliverer = null)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateBookingAndLog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", updatedBooking.BookingID);
            cmd.Parameters.AddWithValue("@rentDate", updatedBooking.PlannedBorrowDate);
            cmd.Parameters.AddWithValue("@returnDate", updatedBooking.PlannedReturnDate);
            cmd.Parameters.AddWithValue("@alterer", alterer);
            cmd.Parameters.AddWithValue("@note", updatedBooking.Notes);
            cmd.Parameters.AddWithValue("@roomNr", updatedBooking.Location.RoomNumber);
            cmd.Parameters.AddWithValue("@buildingName", updatedBooking.Location.Building);
            if (deliverer != null && deliverer != "")
            {
                cmd.Parameters.AddWithValue("@deliverer", deliverer);
            }

            con.Open();
            int bookingLogID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return bookingLogID;
        }

        /// <summary>
        /// Updates the ItemLine and creates a log.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="bookingLogID"></param>
        /// <param name="modelName"></param>
        /// <param name="newQuantity"></param>
        internal void UpdateItemLineAndLog(int bookingID, int bookingLogID, string modelName, int newQuantity)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateItemLineAndLog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            cmd.Parameters.AddWithValue("@bookingLogID", bookingLogID);
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@newQuantity", newQuantity);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



        /// <summary>
        /// ONLY USE IF THE BOOKING HAS NOT BEEN DELIVERED. Does what it says on the tin. No Logs!
        /// </summary>
        /// <param name="deviceID">DeviceID of the BookedDevice</param>
        /// <param name="bookingID">BookingID of the BookedDevice</param>
        internal void DeleteBookedDevice(int deviceID, int bookingID)
        {
            // The delivery for this booking has not been made yet, ergo the bookedDevice may be Deleted. An Undo.
            // "DeleteBookedDevice"

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DeleteBookedDevice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deviceID", deviceID);
            cmd.Parameters.AddWithValue("@bookingID", bookingID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Deletes an ItemLine from a Booking, pre-bookingStart
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="modelName"></param>
        internal void DeleteItemLine(int bookingID, string modelName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DeleteItemLine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            cmd.Parameters.AddWithValue("@modelName", modelName);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Does Everything involved with deleting a Booking: Deleting and Logging the Booking itself and all associated BookedDevices and ItemLines.
        /// </summary>
        /// <param name="bookingID">Booking ID to be deleted</param>
        /// <param name="deleter">Who is deleting this booking</param>
        /// <param name="reason">Why they are deleting it "Cancelled by user", "Ended."</param>
        /// <returns>True if Success</returns>
        internal bool DeleteBooking(int bookingID, string deleter, string reason)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DeleteBooking", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            cmd.Parameters.AddWithValue("@deleter", deleter);
            cmd.Parameters.AddWithValue("@deletionReason", reason);
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int resultPreConversion = Convert.ToInt32(cmd.Parameters["@result"].Value);
            con.Close();

            return resultPreConversion == 1;
        }



        /// <summary>
        /// Counts the current number of devices of ModelName type in storage.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        internal int GetCountDevicesOfModelInStorage(string modelName)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("CountDevicesOfModelInStorage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@modelName", modelName);

            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();

            return result;
        }

        /// <summary>
        /// Counts the maximum theoretical amount of models available in a timespace, for making future bookings.
        /// </summary>
        /// <param name="rentDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        internal int GetModelQuantityAvailable(DateTime rentDate, DateTime returnDate, string modelName)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetModelDeviceQuantityAvailable", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rentDate", rentDate);
            cmd.Parameters.AddWithValue("@returnDate", returnDate);
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.Add("@QuantityOfAvailableDevices", SqlDbType.Int).Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt32(cmd.Parameters["@QuantityOfAvailableDevices"].Value);
            con.Close();

            return result;
        }

        /// <summary>
        /// Counts how many devices Should be available in a period, based on how many devices of the Model that are available minus how many has been booked by the various bookings, excluding this.
        /// </summary>
        /// <param name="rentDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        internal int GetModelQuantityAvailableExcludingBooking(DateTime rentDate, DateTime returnDate, string modelName, int bookingID)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetModelDeviceQuantityAvailableExcludingBooking", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rentDate", rentDate);
            cmd.Parameters.AddWithValue("@returnDate", returnDate);
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@bookingID", bookingID);
            cmd.Parameters.Add("@lowestDeviceQuantity", SqlDbType.Int).Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt32(cmd.Parameters["@lowestDeviceQuantity"].Value);
            con.Close();

            return result;
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
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@modelName", modelName);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                location.ShelfName = (string)reader["shelfName"];
                location.ShelfLevel = (string)reader["shelfLevel"];
                location.ShelfSpot = (string)reader["shelfSpot"];
                location.Location = new BuildingModel(
                        (string)reader["buildingName"],
                        (string)reader["roomNr"]
                    );
            }
            con.Close();

            return location;
        }
    }
}
