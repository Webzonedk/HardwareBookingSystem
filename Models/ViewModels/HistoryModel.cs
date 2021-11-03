using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class HistoryModel
    {
        #region Fields

        private BookingSearchCriteriaModel searchCriteria;
        private List<string> rooms;
        private int bookingID;
        private string customer;
        private string notes;
        private List<ItemLineModel> items;
        private List<DeviceModel> devices;
        private BuildingModel location;
        private DateTime plannedBorrowDate;
        private DateTime plannedReturnDate;
        private string deliveredBy;
        private string changedBy;

        #endregion





        #region Properties

        public BookingSearchCriteriaModel SearchCriteria
        {
            get { return searchCriteria; }
            set { searchCriteria = value; }
        }
        public List<string> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }
        public int BookingID
        {
            get { return bookingID; }
            set { bookingID = value; }
        }
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        public List<ItemLineModel> Items
        {
            get { return items; }
            set { items = value; }
        }
        public List<DeviceModel> Devices
        {
            get { return devices; }
            set { devices = value; }
        }
        public BuildingModel Location
        {
            get { return location; }
            set { location = value; }
        }
        public DateTime PlannedBorrowDate
        {
            get { return plannedBorrowDate; }
            set { plannedBorrowDate = value; }
        }
        public DateTime PlannedReturnDate
        {
            get { return plannedReturnDate; }
            set { plannedReturnDate = value; }
        }
        public string DeliveredBy
        {
            get { return deliveredBy; }
            set { deliveredBy = value; }
        }
        public string ChangedBy
        {
            get { return changedBy; }
            set { changedBy = value; }
        }

        #endregion





        #region Constructors

        public HistoryModel()
        {
            //rooms = new List<string>();
            //items = new List<ItemLineModel>();
            //devices = new List<DeviceModel>();
        }
        #endregion
    }
}
