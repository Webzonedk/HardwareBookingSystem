using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HUS_project.DAL
{
    public class DBManagerDevice
    {
        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerDevice(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        
        internal int CreateDevice(DeviceModel deviceData)
        {
            Debug.WriteLine(connectionString);
            Debug.WriteLine("Server=ANDREASPC; Database=HardwareUdlaanSystem; User Id=Husv1ld; Password=Wh3nY0uN33dSom3th1ng;");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("CreateDevice", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@modelName", System.Data.SqlDbType.VarChar).Value = deviceData.Model.ModelName;
            cmd.Parameters.Add("@modelDescription", System.Data.SqlDbType.VarChar).Value = deviceData.Model.ModelDescription;
            cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = deviceData.Model.Category.Category;
            cmd.Parameters.Add("@changedBy", System.Data.SqlDbType.VarChar).Value = deviceData.ChangedBy;
            cmd.Parameters.Add("@serialNumber", System.Data.SqlDbType.VarChar).Value = deviceData.SerialNumber;
            cmd.Parameters.Add("@deviceID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            //execute query
            cmd.ExecuteNonQuery();

            //return output parameter
            int deviceID = Convert.ToInt32(cmd.Parameters["@deviceID"].Value);
            con.Close();

            return deviceID;
        }


        internal DeviceModel EditDevice(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }
        
        internal DeviceModel EditDeviceLocation(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            

            con.Close();
            return null;
        }

        //get device info from database before edit
        internal DeviceModel GetDeviceInfoWithLocation(int deviceID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetDeviceWithLocation", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@deviceID", System.Data.SqlDbType.Int).Value = deviceID;

            //execute query
            cmd.ExecuteNonQuery();
            
            #region //model for data transfer
            SqlDataReader reader = cmd.ExecuteReader();
            DeviceModel device = new DeviceModel();
            ModelModel model = new ModelModel();
            CategoryModel category = new CategoryModel();
            BuildingModel building = new BuildingModel();
            StorageLocationModel location = new StorageLocationModel();
            model.Category = category;
            location.Location = building;
            #endregion

            while (reader.Read())
            {
                device.DeviceID = (int)reader["deviceID"];
                model.Category.Category = (string)reader["categoryName"];
                model.ModelName = (string)reader["modelName"];
                model.ModelDescription = (string)reader["modelDescription"];

                location.Location.RoomNumber = (byte)reader["roomNr"];
                location.Location.Building = (string)reader["shelfName"];
                location.ShelfName = (string)reader["shelfName"]; ;
                location.ShelfLevel = (byte)reader["shelfLevel"];
                location.ShelfSpot = (byte)reader["shelfSpot"];

            }

            device.Model = model;
            device.Location = location;

            con.Close();
            return device;
        }

        //Get device Logs from database before edit
        internal List<DeviceModel> GetDeviceLogs(int deviceID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetDeviceLogs", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@deviceID", System.Data.SqlDbType.Int).Value = deviceID;

            //execute query
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            List<DeviceModel> Logs = new List<DeviceModel>();
            while (reader.Read())
            {
                DeviceModel device = new DeviceModel();
                ModelModel m = new ModelModel();
                CategoryModel c = new CategoryModel();
                m.Category = c;

                device.DeviceID = (int)reader["deviceID"];
                device.ChangedBy = (string)reader["changedBy"];
                device.ChangeDate = (DateTime)reader["logDate"];
                device.Notes = (string)reader["note"];
                m.Category.Category = (string)reader["categoryName"];
                m.ModelName = (string)reader["modelName"];
               

                device.Model = m;
                Logs.Add(device);
            }

           

            con.Close();
            return Logs;
        }

        internal List<DeviceModel> GetDeviceInventory(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }
    }
}
