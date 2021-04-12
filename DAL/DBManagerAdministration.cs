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

                    BuildingModel building = new BuildingModel((string)reader["buildingName"], 0);

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
        internal List<byte> GetRoomNumbers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectRoomNr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<byte> roomNumbers = new List<byte>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    BuildingModel roomNumber = new BuildingModel(null, (byte)reader["roomNr"]);

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
                    StorageLocationModel shelfName = new StorageLocationModel((string)reader["shelfName"], 0, 0, null);

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
        internal List<byte> GetShelfLevel()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectShelfLevel", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<byte> shelfLevels = new List<byte>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfLevel = new StorageLocationModel(null, (byte)reader["shelfLevel"], 0, null);

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
        internal List<byte> GetShelfSpot()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectShelfSpot", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<byte> shelfSpots = new List<byte>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfSpot = new StorageLocationModel(null, 0, (byte)reader["shelfSpot"], null);

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


            if (dataFromView.StorageLocation.Location.RoomNumber > 0)
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


            if (dataFromView.StorageLocation.ShelfLevel > 0)
            {
                cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = dataFromView.StorageLocation.ShelfLevel;
            }
            else
            {
                cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.TinyInt).Value = null;
            }


            if (dataFromView.StorageLocation.ShelfSpot > 0)
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
                cmd.Parameters.Add("@filter", System.Data.SqlDbType.TinyInt).Value = 1;
            }


            List<StorageLocationModel> selectedStorageLocations = new List<StorageLocationModel>();

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                StorageLocationModel selectedStorageLocation = new StorageLocationModel();
                BuildingModel buildingModel = new BuildingModel();
                buildingModel.Building = (string)reader["buildingName"];
                buildingModel.RoomNumber = (byte)reader["roomNr"];
                selectedStorageLocation.Location = buildingModel;
                selectedStorageLocation.ShelfName = (string)reader["shelfName"];
                selectedStorageLocation.ShelfLevel = (byte)reader["shelfLevel"];
                selectedStorageLocation.ShelfSpot = (byte)reader["shelfSpot"];

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


            if (dataFromView.StorageLocation.Location.RoomNumber > 0)
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
                buildingModel.RoomNumber = (byte)reader["roomNr"];
                selectedRooms.Location = buildingModel;

                selectedRoom.Add(selectedRooms);
            }
            con.Close();
            return selectedRoom;
        }


        internal StorageLocationModel CreateLocation(StorageLocationModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }







        internal List<StorageLocationModel> DeleteLocation(List<StorageLocationModel> dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }



        internal void CreateCategory(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
        }





        internal List<CategoryModel> DeleteLocation(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
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
