using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HUS_project.Models.ViewModels;
using HUS_project.Models;
using System.Diagnostics;

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
        internal CreateBookingModel GetInventory(BookingSearchCriteriaModel SearchModel)
        {
            CreateBookingModel inventory = new CreateBookingModel();

            //set search name to empty if null
            if (SearchModel.SearchName == null)
            {
                SearchModel.SearchName = "";
            }

            inventory.SearchModel = SearchModel;

            //get list of booked items
            List<BookingSearchModel> instockBooked = Select_Models_In_Stock_Existing_Itemline(SearchModel);

            //get list of items in stock and not booked
            List<BookingSearchModel> instocNotkBooked = Select_Models_In_Stock_Not_Existing_Itemline(SearchModel);

            //get list of items booked and not in stock
            List<BookingSearchModel> notInstock = Select_Models_Not_In_Stock_Existing_Itemline(SearchModel);



            //add list to inventory
            AddListToInventory(instocNotkBooked, inventory);
            AddListToInventory(notInstock, inventory);
            AddListToInventory(instockBooked, inventory);

            // remove time if datetime is too long
            //inventory.SearchModel.RentDate = inventory.SearchModel.RentDate.Date;
            //inventory.SearchModel.ReturnDate = inventory.SearchModel.ReturnDate.Date;

            return inventory;
        }


        //gets list of models in stock and exists in itemline
        private List<BookingSearchModel> Select_Models_In_Stock_Existing_Itemline(BookingSearchCriteriaModel inputData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetModelsInstockExistingInItemLine", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (inputData.Category != null)
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = inputData.Category;

            }
            else
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = null;
            }
            cmd.Parameters.Add("@searchName", System.Data.SqlDbType.VarChar).Value = inputData.SearchName;
            cmd.Parameters.Add("@rentDate", System.Data.SqlDbType.DateTime).Value = inputData.RentDate;
            cmd.Parameters.Add("@returnDate", System.Data.SqlDbType.DateTime).Value = inputData.ReturnDate;

            SqlDataReader reader = cmd.ExecuteReader();


            List<BookingSearchModel> inventoryInstock = new List<BookingSearchModel>();

            while (reader.Read())
            {

                BookingSearchModel item = new BookingSearchModel();
                item.ModelID = (int)reader["modelID"];
                item.ModelName = (string)reader["modelName"];
                item.CategoryName = (string)reader["categoryName"];
                item.ReturnDate.Add((DateTime)reader["returnDate"]);
                item.RentDate.Add((DateTime)reader["rentDate"]);
                item.NotInstock.Add((int)reader["Instock"]);

                inventoryInstock.Add(item);
            }


            con.Close();
            return inventoryInstock;
        }

        //gets list of models not in stock and exists in itemline
        private List<BookingSearchModel> Select_Models_Not_In_Stock_Existing_Itemline(BookingSearchCriteriaModel inputData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectModelsExistsInItemLineNotInStock", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (inputData.Category != null)
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = inputData.Category;

            }
            else
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = null;
            }
            cmd.Parameters.Add("@searchName", System.Data.SqlDbType.VarChar).Value = inputData.SearchName;
            cmd.Parameters.Add("@rentDate", System.Data.SqlDbType.DateTime).Value = inputData.RentDate;
            cmd.Parameters.Add("@returnDate", System.Data.SqlDbType.DateTime).Value = inputData.ReturnDate;

            SqlDataReader reader = cmd.ExecuteReader();


            List<BookingSearchModel> inventoryInstock = new List<BookingSearchModel>();

            while (reader.Read())
            {

                BookingSearchModel item = new BookingSearchModel();
                item.ModelID = (int)reader["modelID"];
                item.ModelName = (string)reader["modelName"];
                item.CategoryName = (string)reader["categoryName"];
                item.ReturnDate.Add((DateTime)reader["returnDate"]);
                item.RentDate.Add((DateTime)reader["rentDate"]);
                item.NotInstock.Add((int)reader["quantity"]);

                inventoryInstock.Add(item);
            }


            con.Close();
            return inventoryInstock;
        }

        //gets list of models not in stock and exists in itemline
        private List<BookingSearchModel> Select_Models_In_Stock_Not_Existing_Itemline(BookingSearchCriteriaModel inputData)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectModelsInstockNotExistingInItemLine", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (inputData.Category != null)
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = inputData.Category;

            }
            else
            {
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar).Value = null;
            }
            cmd.Parameters.Add("@searchName", System.Data.SqlDbType.VarChar).Value = inputData.SearchName;
            //cmd.Parameters.Add("@rentDate", System.Data.SqlDbType.DateTime).Value = inputData.RentDate;
            //cmd.Parameters.Add("@returnDate", System.Data.SqlDbType.DateTime).Value = inputData.ReturnDate;

            SqlDataReader reader = cmd.ExecuteReader();


            List<BookingSearchModel> inventoryInstock = new List<BookingSearchModel>();

            while (reader.Read())
            {

                BookingSearchModel item = new BookingSearchModel();
                item.ModelID = (int)reader["modelID"];
                item.ModelName = (string)reader["modelName"];
                item.CategoryName = (string)reader["categoryName"];
                //item.ReturnDate.Add((DateTime)reader["returnDate"]);
                //item.RentDate.Add((DateTime)reader["rentDate"]);
                item.InStock.Add((int)reader["Instock"]);

                inventoryInstock.Add(item);
            }


            con.Close();
            return inventoryInstock;
        }

        private bool CheckExistingModelNames(int modelID, List<BookingSearchModel> comparingList)
        {

            foreach (BookingSearchModel model in comparingList)
            {
                if (model.ModelID == modelID)
                {
                    return true;
                }

            }
            return false;
        }

        private void AddListToInventory(List<BookingSearchModel> inputlist, CreateBookingModel inventory)
        {
            for (int n = 0; n < inputlist.Count; n++)
            {
                int modelID = inputlist[n].ModelID;

                if (CheckExistingModelNames(modelID, inventory.InventoryBooking))
                {
                    for (int j = 0; j < inventory.InventoryBooking.Count; j++)
                    {
                        if (inventory.InventoryBooking[j].ModelID == modelID)
                        {
                            if (inputlist[n].NotInstock.Count > 0)
                            {

                                if (inputlist[n].NotInstock[0] > 0)
                                {
                                    inventory.InventoryBooking[j].RentDate.Add(inputlist[n].RentDate[0]);
                                    inventory.InventoryBooking[j].ReturnDate.Add(inputlist[n].ReturnDate[0]);
                                    inventory.InventoryBooking[j].NotInstock.Add(inputlist[n].NotInstock[0]);
                                }
                            }
                            //inventory.InventoryBooking[j].RentDate.Add(inputlist[n].RentDate[0]);
                            //inventory.InventoryBooking[j].ReturnDate.Add(inputlist[n].ReturnDate[0]);
                        }

                    }
                }
                else
                {
                    inventory.InventoryBooking.Add(inputlist[n]);
                }

            }

        }
    }
}
