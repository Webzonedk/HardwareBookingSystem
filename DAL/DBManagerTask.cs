using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HUS_project.Models;
using System.Data.SqlClient;

namespace HUS_project.DAL
{
    public class DBManagerTask
    {
        //private fields containting connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerTask(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        internal List<BookingModel> BookingDeliveriesToBeMade ()
        {
            List<BookingModel> bookings = new List<BookingModel>();

            return bookings;
        }

        internal List<BookingModel> BookingRetrievalsToBeMade()
        {
            List<BookingModel> bookings = new List<BookingModel>();

            return bookings;
        }
    }
}
