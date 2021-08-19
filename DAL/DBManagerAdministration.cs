using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using HUS_project.Models.ViewModels;
using System.Data.SqlClient;

namespace HUS_project.DAL
{
    public class DBManagerAdministration
    {

        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerAdministration(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        //Getting Building names for the dropdown in Blue Oister bar
        internal List<string> GetBuildings()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectBuildingName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> buildings = new List<string>();
            try
            {

                while (reader.Read())
                {

                    BuildingModel building = new BuildingModel((string)reader["buildingName"], "0");

                    buildings.Add(building.Building);
                }
            }
            catch (Exception)
            {

                throw;
            }

            con.Close();
            return buildings;
        }



        //Getting Room numbers (Not actual rooms) for the dropdown in Blue Oister bar
        internal List<string> GetRoomNumbers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectRoomNr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> roomNumbers = new List<string>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    BuildingModel roomNumber = new BuildingModel(null, (string)reader["roomNr"]);

                    roomNumbers.Add(roomNumber.RoomNumber);
                }
            }
            catch (Exception)
            {

                throw;
            }

            con.Close();
            return roomNumbers;
        }



        //Getting shelf names for the dropdown in Blue Oister bar
        internal List<string> GetShelfName()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectShelfName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> shelfNames = new List<string>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfName = new StorageLocationModel((string)reader["shelfName"], "0", "0", null);

                    shelfNames.Add(shelfName.ShelfName);
                }
            }
            catch (Exception)
            {

                throw;
            }

            con.Close();
            return shelfNames;
        }



        //Getting shelf levels for the dropdown in Blue Oister bar
        internal List<string> GetShelfLevel()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectShelfLevel", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> shelfLevels = new List<string>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfLevel = new StorageLocationModel(null, (string)reader["shelfLevel"], "0", null);

                    shelfLevels.Add(shelfLevel.ShelfLevel);
                }
            }
            catch (Exception)
            {

                throw;
            }

            con.Close();
            return shelfLevels;
        }



        //Getting shelf spots for the dropdown in Blue Oister bar
        internal List<string> GetShelfSpot()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectShelfSpot", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> shelfSpots = new List<string>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfSpot = new StorageLocationModel(null, "0", (string)reader["shelfSpot"], null);

                    shelfSpots.Add(shelfSpot.ShelfSpot);
                }
            }
            catch (Exception)
            {

                throw;
            }

            con.Close();
            return shelfSpots;
        }



        //Getting the list of storagelocations, based on the choices med in the Blue Oister bar.
        internal List<StorageLocationModel> GetSelectedStorageLocations(EditStorageLocationModel dataFromView)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SelectLocationIDBasedOnInputFields", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            if (dataFromView.StorageLocation.Location.Building != null)
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = dataFromView.StorageLocation.Location.Building;
            }
            else
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = null;
            }


            if (dataFromView.StorageLocation.Location.RoomNumber != null)
            {
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = dataFromView.StorageLocation.Location.RoomNumber;
            }
            else
            {
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = null;
            }


            if (dataFromView.StorageLocation.ShelfName != null)
            {
                cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar).Value = dataFromView.StorageLocation.ShelfName;
            }
            else
            {
                cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar).Value = null;
            }


            if (dataFromView.StorageLocation.ShelfLevel != null)
            {
                cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = dataFromView.StorageLocation.ShelfLevel;
            }
            else
            {
                cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = null;
            }


            if (dataFromView.StorageLocation.ShelfSpot != null)
            {
                cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.TinyInt).Value = dataFromView.StorageLocation.ShelfSpot;
            }
            else
            {
                cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.TinyInt).Value = null;
            }


            if (dataFromView.Filter > 0)
            {
                cmd.Parameters.Add("@filter", System.Data.SqlDbType.TinyInt).Value = dataFromView.Filter;
            }
            else
            {
                cmd.Parameters.Add("@filter", System.Data.SqlDbType.TinyInt).Value = 0;
            }


            List<StorageLocationModel> selectedStorageLocations = new List<StorageLocationModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                StorageLocationModel selectedStorageLocation = new StorageLocationModel();
                BuildingModel buildingModel = new BuildingModel();
                buildingModel.Building = (string)reader["buildingName"];
                buildingModel.RoomNumber = (string)reader["roomNr"];
                selectedStorageLocation.Location = buildingModel;
                selectedStorageLocation.LocationID = (int)reader["locationID"];
                selectedStorageLocation.ShelfName = (string)reader["shelfName"];
                selectedStorageLocation.ShelfLevel = (string)reader["shelfLevel"];
                selectedStorageLocation.ShelfSpot = (string)reader["shelfSpot"];

                selectedStorageLocations.Add(selectedStorageLocation);
            }
            con.Close();
            return selectedStorageLocations;
        }


        //Getting specific room, based on the choosen building and roomNr in the mass destruction.
        internal List<StorageLocationModel> GetSpecificRoom(EditStorageLocationModel dataFromView)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SelectRoomBasedOnBuildingNameAndRoomNr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (dataFromView.StorageLocation.Location.Building != null)
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = dataFromView.StorageLocation.Location.Building;
            }
            else
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = null;
            }


            if (dataFromView.StorageLocation.Location.RoomNumber != null)
            {
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = dataFromView.StorageLocation.Location.RoomNumber;
            }
            else
            {
                cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = null;
            }


            List<StorageLocationModel> selectedRoom = new List<StorageLocationModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                StorageLocationModel selectedRooms = new StorageLocationModel();
                BuildingModel buildingModel = new BuildingModel();
                buildingModel.Building = (string)reader["buildingName"];
                buildingModel.RoomNumber = (string)reader["roomNr"];
                selectedRooms.Location = buildingModel;

                selectedRoom.Add(selectedRooms);
            }
            con.Close();
            return selectedRoom;
        }




        //Basic method to get all locations based on the searchTerms//
        internal EditStorageLocationModel GetLocations(EditStorageLocationModel dataFromView)
        {
            List<StorageLocationModel> storageLocations = GetSelectedStorageLocations(dataFromView);
            EditStorageLocationModel searchResult = new EditStorageLocationModel();

            List<string> buildings = GetBuildings();
            List<string> roomNumbers = GetRoomNumbers();
            List<string> shelfNames = GetShelfName();
            List<string> shelfLevels = GetShelfLevel();
            List<string> shelfspots = GetShelfSpot();


            searchResult.Buildings = buildings;
            searchResult.RoomNumbers = roomNumbers;
            searchResult.ShelfNames = shelfNames;
            searchResult.ShelfLevels = shelfLevels;
            searchResult.ShelfSpots = shelfspots;
            searchResult.Filter = 0;

            searchResult.StorageLocations = storageLocations;
            searchResult.StorageLocation = dataFromView.StorageLocation;
            return searchResult;
        }



        //Method to Delete a single location based on locationID
        internal string DeleteLocation(int locationID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteStorageLocation", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@locationID", System.Data.SqlDbType.Int).Value = locationID;
            cmd.Parameters.Add("@alert", System.Data.SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string alert = "empty";
            if (cmd.Parameters["@alert"].Value != System.DBNull.Value)
            {
                alert = (string)cmd.Parameters["@alert"].Value;
            }
            else
            {
                alert = "empty";
            }



            con.Close();
            return alert;
        }




        //Method to create a location based on the input fields in the Blue Oister Bar
        internal EditStorageLocationModel CreateLocation(EditStorageLocationModel storagelocation)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("CreateOrActivateInactivateStorageLocationAndRooms", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (storagelocation.StorageLocation.Location.Building != null)
            {
                cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = storagelocation.StorageLocation.Location.Building;
            }


            cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.TinyInt).Value = storagelocation.StorageLocation.Location.RoomNumber;

            if (storagelocation.StorageLocation.ShelfName != null)
            {
                cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar).Value = storagelocation.StorageLocation.ShelfName;
            }


            cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = storagelocation.StorageLocation.ShelfLevel;

            cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.TinyInt).Value = storagelocation.StorageLocation.ShelfSpot;
            try
            {
            cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
            }

            con.Close();
            return storagelocation;
        }






        internal void CreateCategory(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
        }






        internal void DeleteCategory(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
        }



        internal void EditCategory(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
        }




    }
}
