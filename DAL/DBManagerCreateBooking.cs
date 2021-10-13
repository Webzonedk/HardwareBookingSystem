using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HUS_project.Models.ViewModels;
using HUS_project.Models;

namespace HUS_project.DAL
{
    public class DBManagerCreateBooking
    {
        //private fields containing connectionstrings for databases
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //constructor setting connectionstrings to databases
        public DBManagerCreateBooking(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");

        }

        //get list of all models within a date
        //internal ModelInfoModel GetInventory(ModelInfoModel SearchModel)
        //{
            
        //}


        //internal BookingSearchModel Select_Models_In_Stock_Existing_Itemline(BookingSearchCriteriaModel inputData)
        //{
        //    SqlConnection con = new SqlConnection(connectionString);
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("GetModelsInstockExistingInItemLine", con);
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = inputData.Category;





        //    int counter = 0;
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DeviceModel device = new DeviceModel();
        //        ModelModel model = new ModelModel();
        //        CategoryModel category = new CategoryModel();
        //        StorageLocationModel location = new StorageLocationModel();
        //        BuildingModel building = new BuildingModel();

        //        device.DeviceID = (int)reader["deviceID"];


        //        if (reader.IsDBNull(reader.GetOrdinal("returnDate")))
        //        {
        //            device.BookingStatus = "På Lager";
        //        }
        //        else
        //        {
        //            DateTime returnDate = (DateTime)reader["returnDate"];
        //            DateTime rentDate = (DateTime)reader["rentDate"];
        //            device.BookingStatus = $"{rentDate.ToShortDateString()}-{returnDate.ToShortDateString()}";

        //        }
        //        category.Category = (string)reader["categoryName"];
        //        model.ModelName = (string)reader["modelName"];

        //        if (!reader.IsDBNull(reader.GetOrdinal("bookedBuilding")))
        //        {

        //        }
        //        else
        //        {
        //            building.RoomNumber = (string)reader["roomNr"];
        //            building.Building = (string)reader["buildingName"];
        //            location.ShelfName = (string)reader["shelfName"]; ;
        //            location.ShelfLevel = (string)reader["shelfLevel"];
        //            location.ShelfSpot = (string)reader["shelfSpot"];

        //        }

        //        model.Category = category;
        //        device.Model = model;
        //        location.Location = building;
        //        device.Location = location;
        //      //  SearchModel.BorrowedDevices.Add(device);
        //        counter++;
        //    }


        //    con.Close();
        //    return SearchModel;
        //}
    }
}
