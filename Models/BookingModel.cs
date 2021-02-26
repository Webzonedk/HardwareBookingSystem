using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class BookingModel
    {
        #region Fields

        private int bookingID;
        private string customer;
        private List<ItemLineModel> items;
        private List<DeviceModel> devices;
        private BuildingModel location;
        private DateTime plannedBorrowDate;
        private DateTime plannedReturnDate;
        private string deliveredBy;
        private int bookingStatus;

        #endregion



        #region Properties

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
            get { return this.plannedBorrowDate; }
            set { this.plannedBorrowDate = value; }
        }

        public DateTime PlannedReturnDate
        {
            get { return this.plannedReturnDate; }
            set { this.plannedReturnDate = value; }
        }

        public string DeliveredBy
        {
            get{  return deliveredBy; }
            set{ deliveredBy = value;}
        }


        public int BookingStatus
        {
            get { return bookingStatus; }
            set { bookingStatus = value; }
        }

        #endregion



        #region Construtors

        public BookingModel()
        {

        }

        public BookingModel(int BookingID, string Customer, List<ItemLineModel> items, List<DeviceModel> devices, BuildingModel location, DateTime PlannedBorrowDate, DateTime PlannedReturnDate, int bookingStatus, string deliveredBy = null)
        {
            this.bookingID = BookingID;
            this.customer = Customer;
            this.items = items;
            this.devices = devices;
            this.location = location;
            this.plannedBorrowDate = PlannedBorrowDate;
            this.plannedReturnDate = PlannedReturnDate;
            this.bookingStatus = bookingStatus;
            this.deliveredBy = deliveredBy;
        }

        #endregion
    }
}
