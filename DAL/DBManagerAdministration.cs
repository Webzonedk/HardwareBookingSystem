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
        //----------------------------------------------------------------------------
        //private fields containting connectionstrings for databases
        //----------------------------------------------------------------------------
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerAdministration(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        //----------------------------------------------------------------------------
        //Getting Building names for the dropdown in Blue Oister bar
        //----------------------------------------------------------------------------
        internal List<string> GetBuildings()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectBuildingName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> buildings = new List<string>();

                while (reader.Read())
                {

                    BuildingModel building = new BuildingModel((string)reader["buildingName"], "0");

                    buildings.Add(building.Building);
                }
                con.Close();
                return buildings;
            }
            finally
            {

            }


        }



        //----------------------------------------------------------------------------
        //Getting Room numbers (Not actual rooms) for the dropdown in Blue Oister bar
        //----------------------------------------------------------------------------
        internal List<string> GetRoomNumbers()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectRoomNr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> roomNumbers = new List<string>();

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    BuildingModel roomNumber = new BuildingModel(null, (string)reader["roomNr"]);

                    roomNumbers.Add(roomNumber.RoomNumber);
                }
                con.Close();
                return roomNumbers;
            }
            finally
            {

            }


        }


        //----------------------------------------------------------------------------
        //Getting shelf names for the dropdown in Blue Oister bar
        //----------------------------------------------------------------------------
        internal List<string> GetShelfName()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectShelfName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> shelfNames = new List<string>();

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfName = new StorageLocationModel((string)reader["shelfName"], "0", "0", null);

                    shelfNames.Add(shelfName.ShelfName);
                }
                con.Close();
                return shelfNames;
            }
            finally
            {

            }


        }



        //----------------------------------------------------------------------------
        //Getting shelf levels for the dropdown in Blue Oister bar
        //----------------------------------------------------------------------------
        internal List<string> GetShelfLevel()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectShelfLevel", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> shelfLevels = new List<string>();

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfLevel = new StorageLocationModel(null, (string)reader["shelfLevel"], "0", null);

                    shelfLevels.Add(shelfLevel.ShelfLevel);
                }
                con.Close();
                return shelfLevels;
            }
            finally
            {

            }


        }


        //----------------------------------------------------------------------------
        //Getting shelf spots for the dropdown in Blue Oister bar
        //----------------------------------------------------------------------------
        internal List<string> GetShelfSpot()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectShelfSpot", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> shelfSpots = new List<string>();

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfSpot = new StorageLocationModel(null, "0", (string)reader["shelfSpot"], null);

                    shelfSpots.Add(shelfSpot.ShelfSpot);
                }
                con.Close();
                return shelfSpots;
            }
            finally
            {

            }


        }




        //----------------------------------------------------------------------------
        //Getting the complete list of storagelocations, based on the choices med in the Blue Oister bar to be shown in LocationAdmin view.
        //----------------------------------------------------------------------------
        internal List<StorageLocationModel> GetSelectedStorageLocations(EditStorageLocationModel dataFromView)
        {
            try
            {


                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SelectLocationIDBasedOnInputFields", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                if (dataFromView.StorageLocation.Location.Building != null)
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 50).Value = dataFromView.StorageLocation.Location.Building.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 50).Value = null;
                }


                if (dataFromView.StorageLocation.Location.RoomNumber != null)
                {
                    cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.Location.RoomNumber.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.VarChar, 10).Value = null;
                }


                if (dataFromView.StorageLocation.ShelfName != null)
                {
                    cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.ShelfName.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar, 10).Value = null;
                }


                if (dataFromView.StorageLocation.ShelfLevel != null)
                {
                    cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.ShelfLevel.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.VarChar, 10).Value = null;
                }


                if (dataFromView.StorageLocation.ShelfSpot != null)
                {
                    cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.ShelfSpot.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.VarChar, 10).Value = null;
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
            finally
            {

            }
        }




        //----------------------------------------------------------------------------
        //Getting specific room, based on the choosen building and roomNr in the mass destruction.
        //----------------------------------------------------------------------------
        internal List<StorageLocationModel> GetSpecificRoom(EditStorageLocationModel dataFromView)
        {
            try
            {


                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SelectRoomBasedOnBuildingNameAndRoomNr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (dataFromView.StorageLocation.Location.Building != null)
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.Location.Building.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 10).Value = null;
                }


                if (dataFromView.StorageLocation.Location.RoomNumber != null)
                {
                    cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.StorageLocation.Location.RoomNumber.ToUpper();
                }
                else
                {
                    cmd.Parameters.Add("@roomNr", System.Data.SqlDbType.VarChar, 10).Value = null;
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
            finally
            {

            }
        }




        //----------------------------------------------------------------------------
        //Method to get all building, room numbers, shelf names, shelf levels and shelf spots to the drop downs in the Blue Oister bar//
        //----------------------------------------------------------------------------
        internal EditStorageLocationModel GetLocations(EditStorageLocationModel dataFromView)
        {
            try
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
                searchResult.HiddenFieldID = dataFromView.HiddenFieldID;

                searchResult.StorageLocations = storageLocations;
                searchResult.StorageLocation = dataFromView.StorageLocation;
                return searchResult;
            }
            finally
            {

            }
        }




        //----------------------------------------------------------------------------
        //Method to get all locations based on the searchTerms//
        //----------------------------------------------------------------------------
        internal EditStorageLocationModel GetSpecificStorageLocation(string dataFromView)
        {
            try
            {
                EditStorageLocationModel selectedStorageLocation = new EditStorageLocationModel();
                StorageLocationModel storageLocation = new StorageLocationModel();
                BuildingModel building = new BuildingModel();
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectStorageLocationBasedOnID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@locationID", System.Data.SqlDbType.Int).Value = int.Parse(dataFromView);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    building.Building = (string)reader["buildingName"];
                    building.RoomNumber = (string)reader["roomNr"];
                    storageLocation.ShelfName = (string)reader["shelfName"];
                    storageLocation.ShelfLevel = (string)reader["shelfLevel"];
                    storageLocation.ShelfSpot = (string)reader["shelfSpot"];
                }
                storageLocation.Location = building;
                selectedStorageLocation.StorageLocation = storageLocation;

                return selectedStorageLocation;
            }
            finally
            {

            }

        }




        //----------------------------------------------------------------------------
        //Method to create a location based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateLocation(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateStorageLocation", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createStorageLocationFeedBack;
                if (storagelocation.StorageLocation.Location.Building != null && storagelocation.StorageLocation.Location.RoomNumber != null && storagelocation.StorageLocation.ShelfName != null && storagelocation.StorageLocation.ShelfLevel != null && storagelocation.StorageLocation.ShelfSpot != null)
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 50).Value = storagelocation.StorageLocation.Location.Building.ToUpper();
                    cmd.Parameters.Add("@roomNumber", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.Location.RoomNumber.ToUpper();
                    cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfName.ToUpper();
                    cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfLevel.ToUpper();
                    cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfSpot.ToUpper();
                    cmd.Parameters.Add("@createStorageLocationFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createStorageLocationFeedBack"].Value != System.DBNull.Value)
                {
                    createStorageLocationFeedBack = (string)cmd.Parameters["@createStorageLocationFeedBack"].Value;
                }
                else
                {
                    createStorageLocationFeedBack = null;
                }
                con.Close();
                return createStorageLocationFeedBack;
            }
            finally
            {

            }

        }



        //----------------------------------------------------------------------------
        //Method to create a building based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateBuilding(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateBuilding", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createBuildingFeedBack;
                if (storagelocation.StorageLocation.Location.Building != null)
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 50).Value = storagelocation.StorageLocation.Location.Building.ToUpper();
                    cmd.Parameters.Add("@createBuildingFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createBuildingFeedBack"].Value != System.DBNull.Value)
                {
                    createBuildingFeedBack = (string)cmd.Parameters["@createBuildingFeedBack"].Value;
                }
                else
                {
                    createBuildingFeedBack = null;
                }
                con.Close();
                return createBuildingFeedBack;
            }
            finally
            {

            }
        }



        //----------------------------------------------------------------------------
        //Method to create a room number based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateRoomNumber(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateRoomNumber", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createRoomNumberFeedBack;
                if (storagelocation.StorageLocation.Location.RoomNumber != null)
                {
                    cmd.Parameters.Add("@roomNumber", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.Location.RoomNumber.ToUpper();
                    cmd.Parameters.Add("@createRoomNumberFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;

                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createRoomNumberFeedBack"].Value != System.DBNull.Value)
                {
                    createRoomNumberFeedBack = (string)cmd.Parameters["@createRoomNumberFeedBack"].Value;
                }
                else
                {
                    createRoomNumberFeedBack = null;
                }
                con.Close();
                return createRoomNumberFeedBack;
            }
            finally
            {

            }


        }




        //----------------------------------------------------------------------------
        //Method to create a room based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateRoom(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateRoom", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createRoomFeedBack;
                if (storagelocation.StorageLocation.Location.Building != null && storagelocation.StorageLocation.Location.RoomNumber != null)
                {
                    cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar, 50).Value = storagelocation.StorageLocation.Location.Building.ToUpper();
                    cmd.Parameters.Add("@roomNumber", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.Location.RoomNumber.ToUpper();
                    cmd.Parameters.Add("@createRoomFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createRoomFeedBack"].Value != System.DBNull.Value)
                {
                    createRoomFeedBack = (string)cmd.Parameters["@createRoomFeedBack"].Value;
                }
                else
                {
                    createRoomFeedBack = null;
                }
                con.Close();
                return createRoomFeedBack;
            }
            finally
            {

            }


        }




        //----------------------------------------------------------------------------
        //Method to create a shelf Name based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateShelfName(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateShelfName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createShelfNameFeedBack;
                if (storagelocation.StorageLocation.ShelfName != null)
                {
                    cmd.Parameters.Add("@shelfName", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfName.ToUpper();
                    cmd.Parameters.Add("@createShelfNameFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createShelfNameFeedBack"].Value != System.DBNull.Value)
                {
                    createShelfNameFeedBack = (string)cmd.Parameters["@createShelfNameFeedBack"].Value;
                }
                else
                {
                    createShelfNameFeedBack = null;
                }
                con.Close();
                return createShelfNameFeedBack;
            }
            finally
            {

            }
        }




        //----------------------------------------------------------------------------
        //Method to create a shelf Name based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateShelfLevel(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateShelfLevel", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createShelfLevelFeedBack;
                if (storagelocation.StorageLocation.ShelfLevel != null)
                {
                    cmd.Parameters.Add("@shelfLevel", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfLevel.ToUpper();
                    cmd.Parameters.Add("@createShelfLevelFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createShelfLevelFeedBack"].Value != System.DBNull.Value)
                {
                    createShelfLevelFeedBack = (string)cmd.Parameters["@createShelfLevelFeedBack"].Value;
                }
                else
                {
                    createShelfLevelFeedBack = null;
                }
                con.Close();
                return createShelfLevelFeedBack;
            }
            finally
            {

            }
        }




        //----------------------------------------------------------------------------
        //Method to create a shelf Name based on the input fields in the Blue Oister Bar
        //----------------------------------------------------------------------------
        internal string CreateShelfSpot(EditStorageLocationModel storagelocation)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateShelfSpot", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string createShelfSpotFeedBack;
                if (storagelocation.StorageLocation.ShelfSpot != null)
                {
                    cmd.Parameters.Add("@shelfSpot", System.Data.SqlDbType.VarChar, 10).Value = storagelocation.StorageLocation.ShelfSpot.ToUpper();
                    cmd.Parameters.Add("@createShelfSpotFeedBack", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@createShelfSpotFeedBack"].Value != System.DBNull.Value)
                {
                    createShelfSpotFeedBack = (string)cmd.Parameters["@createShelfSpotFeedBack"].Value;
                }
                else
                {
                    createShelfSpotFeedBack = null;
                }
                con.Close();
                return createShelfSpotFeedBack;
            }
            finally
            {

            }

        }









        //----------------------------------------------------------------------------
        //Method to Delete a single location based on locationID
        //----------------------------------------------------------------------------
        internal string DeleteLocation(int locationID)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteStorageLocation", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@locationID", System.Data.SqlDbType.Int).Value = locationID;
                cmd.Parameters.Add("@alert", System.Data.SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string alert;
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
            finally
            {

            }

        }




        //----------------------------------------------------------------------------
        //Method to Delete a building, based on the input fields in massdestruction area
        //----------------------------------------------------------------------------
        internal string DeleteBuilding(EditStorageLocationModel dataFromView)
        {
            try
            {
                string deleteFeedback = null;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteBuilding", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@buildingNameToDelete", System.Data.SqlDbType.VarChar, 50).Value = dataFromView.DeleteBuilding.ToUpper();
                cmd.Parameters.Add("@deleteFeedback", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@deleteFeedback"].Value != System.DBNull.Value)
                {
                    deleteFeedback = (string)cmd.Parameters["@deleteFeedback"].Value;
                }
                con.Close();
                return deleteFeedback;
            }
            finally
            {

            }

        }




        //----------------------------------------------------------------------------
        //Method to Delete a roomNumber, based on the input fields in massdestruction area
        //----------------------------------------------------------------------------
        internal string DeleteRoomNumber(EditStorageLocationModel dataFromView)
        {
            try
            {
                string deleteFeedback = null;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteRoomNumber", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@RoomNumberToDelete", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.DeleteRoomNumber.ToUpper();
                cmd.Parameters.Add("@deleteFeedback", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@deleteFeedback"].Value != System.DBNull.Value)
                {
                    deleteFeedback = (string)cmd.Parameters["@deleteFeedback"].Value;
                }
                con.Close();
                return deleteFeedback;
            }
            finally
            {

            }

        }




        //----------------------------------------------------------------------------
        //Method to Delete a Room, based on the input fields in massdestruction area
        //----------------------------------------------------------------------------
        internal string DeleteRoom(EditStorageLocationModel dataFromView)
        {
            try
            {
                string deleteFeedback = null;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteRoom", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@buildingNameToDelete", System.Data.SqlDbType.VarChar, 50).Value = dataFromView.DeleteBuilding.ToUpper();
                cmd.Parameters.Add("@RoomNumberToDelete", System.Data.SqlDbType.VarChar, 10).Value = dataFromView.DeleteRoomNumber.ToUpper();
                cmd.Parameters.Add("@RoomToDelete", System.Data.SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@deleteFeedback", System.Data.SqlDbType.VarChar, 200).Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@deleteFeedback"].Value != System.DBNull.Value)
                {
                    deleteFeedback = (string)cmd.Parameters["@deleteFeedback"].Value;
                }
                con.Close();
                return deleteFeedback;
            }
            finally
            {

            }

        }





        /// <summary>
        /// Returns True if the Room exists.
        /// </summary>
        /// <param name="bM">Building Model, containing RoomNumber and BuildingName</param>
        /// <returns>Returns True if the Room exists.</returns>
        internal bool DoesRoomExist(BuildingModel bM)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("DoesRoomExist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@roomNumber", bM.RoomNumber);
                cmd.Parameters.AddWithValue("@buildingName", bM.Building);

                con.Open();
                bool result = (int)cmd.ExecuteScalar() == 1;
                con.Close();

                return result;

            }
            finally
            {

            }
        }


    }
}
