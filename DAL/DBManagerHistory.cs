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

    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //This DBManager is not in use, as it has been cut away for version 1.0
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    public class DBManagerHistory
    {
        //----------------------------------------------------------------------------
        //private fields containting connectionstrings for databases
        //----------------------------------------------------------------------------
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerHistory(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        //----------------------------------------------------------------------------
        //Getting OldBookings
        //----------------------------------------------------------------------------
        internal List<HistoryModel> GetReturnedBoookings()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("GetReturnedBoookings", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;




                List<HistoryModel> bookings = new List<HistoryModel>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   
                }
                con.Close();
                return bookings;
            }
            finally
            {

            }


        }



    }
}
