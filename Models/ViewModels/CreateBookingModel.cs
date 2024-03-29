﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class CreateBookingModel
    {
        private BookingSearchCriteriaModel searchModel;
        private List<string> categoryDropdown;
        private List<string> locationDropdown;
        private List<BookingSearchModel> inventoryBooking;
        private List<ItemLineModel> itemLines;
        private BookingModel bookingOrder;
        private string notes;
        private bool datevalidated;

        public bool DateValidated
        {
            get { return datevalidated; }
            set { datevalidated = value; }
        }


        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        public BookingModel BookingOrder
        {
            get { return bookingOrder; }
            set { bookingOrder = value; }
        }

        private string location;
        private string modelName;
        private int modelID;

        public int ModelID
        {
            get { return modelID; }
            set { modelID = value; }
        }


        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        private int basketCount;

        public int BasketCount
        {
            get { return basketCount; }
            set { basketCount = value; }
        }




        public string Location
        {
            get { return location; }
            set { location = value; }
        }



        public List<ItemLineModel> ItemLines
        {
            get { return itemLines; }
            set { itemLines = value; }
        }

        public BookingSearchCriteriaModel SearchModel
        {
            get { return searchModel; }
            set { searchModel = value; }
        }

        public List<BookingSearchModel> InventoryBooking
        {
            get { return inventoryBooking; }
            set { inventoryBooking = value; }
        }

        public List<string> CategoryDropdown { get => categoryDropdown; set => categoryDropdown = value; }
        public List<string> LocationDropdown { get => locationDropdown; set => locationDropdown = value; }

        public CreateBookingModel()
        {
            inventoryBooking = new List<BookingSearchModel>();
            itemLines = new List<ItemLineModel>();
            categoryDropdown = new List<string>();
            locationDropdown = new List<string>();
            SearchModel = new BookingSearchCriteriaModel(null, "", DateTime.Now.AddDays(1), DateTime.Now.AddDays(14));

        }
    }
}
