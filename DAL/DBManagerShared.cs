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

        internal List<CategoryModel> GetCategories(string dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }

        internal List<ModelModel> GetModelNames(DeviceModel dummy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("StoredProcedureName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            con.Close();
            return null;
        }

        internal List<BuildingModel> GetAllRooms(DeviceModel dummy)
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
