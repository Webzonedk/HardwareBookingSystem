using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
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


        internal List<EditStorageLocationModel> GetDropDowns(string seachInputBuilding)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectBuildingName ", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@buildingName", System.Data.SqlDbType.VarChar).Value = seachInputBuilding;
            //List<EditStorageLocationModel> dropDownData = new List<EditStorageLocationModel>();
            
            //Execute the input parametre query
            cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                List<EditStorageLocationModel> dropDownData = new List<EditStorageLocationModel>();
            try
            {

                while (reader.Read())
                {
                    //EditStorageLocationModel output = new EditStorageLocationModel();
                    EditStorageLocationModel building = new EditStorageLocationModel();
                    building.StorageLocation.Location.Building = (string)reader["buildingName"];

                    dropDownData.Add(building);
                }
            }
            catch (Exception)
            {

                throw;
            }



            con.Close();
            return dropDownData;
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



        internal List<CategoryModel> DeleteLocation(string dummy)
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
