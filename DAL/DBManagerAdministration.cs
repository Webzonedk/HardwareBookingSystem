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




        internal List<byte> GetShelfLevel()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectRoomNr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<byte> shelfLevels = new List<byte>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfLevel = new StorageLocationModel(null, (byte)reader["roomNr"], 0, null);

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




        internal List<byte> GetShelfSpot()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectRoomNr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<byte> shelfLevels = new List<byte>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    StorageLocationModel shelfLevel = new StorageLocationModel(null, (byte)reader["roomNr"], 0, null);

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




        internal List<string> ee;




        internal StorageLocationModel CreateLocation(StorageLocationModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }



        internal StorageLocationModel DeleteLocation(StorageLocationModel dummy)
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
