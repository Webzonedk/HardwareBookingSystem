﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models.ViewModels;
using HUS_project.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

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
            //  Debug.WriteLine("Server=ANDREASPC; Database=HardwareUdlaanSystem; User Id=Husv1ld; Password=Wh3nY0uN33dSom3th1ng;");
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
            cmd.Parameters.Add("@modelID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
            //execute query
            cmd.ExecuteNonQuery();

            //return output parameter
            int deviceID = Convert.ToInt32(cmd.Parameters["@deviceID"].Value);
            int modelID = Convert.ToInt32(cmd.Parameters["@modelID"].Value);
            con.Close();



            //rename image - based on model ID
            string root = (string)AppDomain.CurrentDomain.GetData("webRootPath");
            string webroot = root + "\\DeviceContent";
            string oldName = "\\Capture.png";
            string newfilename = "\\Capture_" + modelID + ".png";

            //check if image path exist
            if (File.Exists($"{webroot}{oldName}"))
            {
                //rename file
                try
                {
                    File.Move($"{webroot}{oldName}", $"{webroot}{newfilename}");
                }
                catch (Exception)
                {
                    File.Delete($"{webroot}{newfilename}");
                     File.Move($"{webroot}{oldName}", $"{webroot}{newfilename}");

                    //throw;
                }
            }

            

            return deviceID;
        }


        internal int EditDevice(EditDeviceModel deviceData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("EditDevice", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@modelName", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Model.ModelName;
            cmd.Parameters.Add("@modelDescription", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Model.ModelDescription;
            cmd.Parameters.Add("@categoryName", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Model.Category.Category;
            cmd.Parameters.Add("@deviceID", System.Data.SqlDbType.Int).Value = deviceData.Device.DeviceID;
            cmd.Parameters.Add("@status", System.Data.SqlDbType.TinyInt).Value = deviceData.Device.Status;
            cmd.Parameters.Add("@note", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Notes;
            cmd.Parameters.Add("@changedBy", System.Data.SqlDbType.VarChar).Value = deviceData.Device.ChangedBy;
            int success = cmd.ExecuteNonQuery();

            if (success > 0)
            {
                success = 1;
            }

            con.Close();
            return success;
        }

        //edit device location
        internal EditDeviceModel EditDeviceLocation(EditDeviceModel deviceData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("EditDeviceLocation", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Location.Location.Building;
            cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = deviceData.Device.Location.Location.RoomNumber;
            cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Location.ShelfName;
            cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = deviceData.Device.Location.ShelfLevel;
            cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.TinyInt).Value = deviceData.Device.Location.ShelfSpot;
            cmd.Parameters.Add("@changedBy", System.Data.SqlDbType.VarChar).Value = deviceData.Device.ChangedBy;
            cmd.Parameters.Add("@note", System.Data.SqlDbType.VarChar).Value = deviceData.Device.Notes;
            cmd.Parameters.Add("@deviceID", System.Data.SqlDbType.Int).Value = deviceData.Device.DeviceID;
            cmd.ExecuteNonQuery();
            con.Close();
            return deviceData;
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
            //   cmd.ExecuteNonQuery();

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
                device.SerialNumber = (string)reader["serialNumber"];
                device.Status = (byte)reader["status"];
                model.Category.Category = (string)reader["categoryName"];
                model.ModelName = (string)reader["modelName"];
                model.ModelDescription = (string)reader["modelDescription"];

                location.Location.RoomNumber = (byte)reader["roomNr"];
                location.Location.Building = (string)reader["buildingName"];
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

            SqlDataReader reader = cmd.ExecuteReader();
            List<DeviceModel> Logs = new List<DeviceModel>();
            while (reader.Read())
            {
                DeviceModel device = new DeviceModel();
                ModelModel m = new ModelModel();
                CategoryModel c = new CategoryModel();
                m.Category = c;

            
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

        //Get storagelocations from database when changing room
        internal EditDeviceModel GetStorageLocations(EditDeviceModel editData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetStorageLocation", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (editData != null)
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = editData.Device.Location.Location.Building;
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.Int).Value = (int)editData.Device.Location.Location.RoomNumber;
            }
            else
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = null;
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.Int).Value = null;
            }


            //get data
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> storageLocations = new List<string>();
            List<string> Rooms = new List<string>();

            //get shelves & rooms
            while (reader.Read())
            {
                string location = new string($"{(string)reader["shelfName"]}.{(byte)reader["shelfLevel"]}.{(byte)reader["shelfSpot"]}");
                storageLocations.Add(location);

                if (editData == null)
                {

                    //add rooms 
                    string room = new string($"{(string)reader["buildingName"]}.{(byte)reader["roomNr"]}");
                    if (Rooms.Count <= 0)
                    {
                        Rooms.Add(room);

                    }
                    //add room to list of not the same
                    else
                    {
                        if (!string.Equals(room, Rooms[Rooms.Count - 1]))
                        {
                            Rooms.Add(room);
                        }
                    }
                }

            }



            EditDeviceModel data = new EditDeviceModel();
            data.Rooms = Rooms;
            data.Shelfs = storageLocations;


            con.Close();
            return data;
        }


        // get all devices based on search query
        internal ModelInfoModel GetDeviceInventory(ModelInfoModel SearchModel)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SearchDevices", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (SearchModel.Category != null)
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = SearchModel.Category;

            }
            else
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = null;
            }

            if (SearchModel.Filter > 0)
            {
                cmd.Parameters.Add("@filter", System.Data.SqlDbType.TinyInt).Value = SearchModel.Filter;

            }
            else
            {
                cmd.Parameters.Add("@filter", System.Data.SqlDbType.TinyInt).Value = null;
            }

            if (SearchModel.SearchName != null)
            {
                cmd.Parameters.Add("@searchName", System.Data.SqlDbType.VarChar).Value = SearchModel.SearchName;

            }
            else
            {
                cmd.Parameters.Add("@searchName", System.Data.SqlDbType.VarChar).Value = null;
            }

            if (SearchModel.InStock > 0)
            {
                cmd.Parameters.Add("@inStock", System.Data.SqlDbType.TinyInt).Value = SearchModel.InStock;

            }
            else
            {
                cmd.Parameters.Add("@inStock", System.Data.SqlDbType.TinyInt).Value = null;
            }


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DeviceModel device = new DeviceModel();
                ModelModel model = new ModelModel();
                CategoryModel category = new CategoryModel();
                StorageLocationModel location = new StorageLocationModel();
                BuildingModel building = new BuildingModel();

                device.DeviceID = (int)reader["deviceID"];
                category.Category = (string)reader["categoryName"];
                model.ModelName = (string)reader["modelName"];


                building.RoomNumber = (byte)reader["roomNr"];
                building.Building = (string)reader["buildingName"];
                location.ShelfName = (string)reader["shelfName"]; ;
                location.ShelfLevel = (byte)reader["shelfLevel"];
                location.ShelfSpot = (byte)reader["shelfSpot"];

                model.Category = category;
                device.Model = model;
                location.Location = building;
                device.Location = location;
                SearchModel.BorrowedDevices.Add(device);
            }


            con.Close();
            return SearchModel;
        }
    }
}
