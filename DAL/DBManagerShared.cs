using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;

namespace HUS_project.DAL
{
    public class DBManagerShared
    {
        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerShared(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        internal List<string> GetCategories()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectAllCategories", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();



            SqlDataReader reader = cmd.ExecuteReader();
            List<string> categories = new List<string>();
            while (reader.Read())
            {
                CategoryModel category = new CategoryModel((string)reader["categoryName"]);
                categories.Add(category.Category);
            }

            con.Close();
            return categories;
        }

        internal List<string> GetModelNames()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetAllModels", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
          //  cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> modelNames = new List<string>();
            while (reader.Read())
            {
                ModelModel modelName = new ModelModel((string)reader["modelName"], null, null);
                modelNames.Add(modelName.ModelName);
            }

            con.Close();
            return modelNames;
        }

        internal List<string> GetAllRooms()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetAllRooms", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
          //  cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> rooms = new List<string>();
            while (reader.Read())
            {
                int roomNr = (byte)reader["roomNr"];
                string room = (string)reader["buildingName"] + "." + roomNr.ToString();
                rooms.Add(room);
            }


            con.Close();
            return rooms;
        }

        internal int GetAvailableDeviceQuantities(DeviceModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetModelDeviceQuantityAvailable", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();

            //set quantity from query
            int quantity = (int)reader["QuantityOfAvailableDevices"];

            con.Close();
            return quantity;
        }

        internal BookingModel GetUserBookingsClosed(DeviceModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetUserBookingsClosed", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }

        internal BookingModel GetUserBookingsCurrent(DeviceModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetUserBookingsCurrent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }

        internal BookingModel GetUserBookingsOpen(DeviceModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetUserBookingsOpen", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            

            con.Close();
            return null;
        }

        internal int GetModelID(string modelName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetModelName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@modelName", System.Data.SqlDbType.VarChar).Value = modelName;
            cmd.Parameters.Add("@modelID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            //execute query
            cmd.ExecuteNonQuery();
           
            //return output parameter
            int modelID = Convert.ToInt32(cmd.Parameters["@modelID"].Value);

           
            con.Close();
            return modelID;
        }
    }
}
