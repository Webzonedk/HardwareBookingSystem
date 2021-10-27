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
        private string notes;
        private List<ItemLineModel> items;
        private List<DeviceModel> devices;
        private BuildingModel location;
        private DateTime plannedBorrowDate;
        private DateTime plannedReturnDate;
        private string deliveredBy;

        #endregion



        #region Properties

        public int BookingID
        {
            get { return this.bookingID; }
            set { this.bookingID = value; }
        }

        public string Customer
        {
            get { return this.customer; }
            set { this.customer = value; }
        }

        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        public List<ItemLineModel> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

        public List<DeviceModel> Devices
        {
            get { return this.devices; }
            set { this.devices = value; }
        }


        public BuildingModel Location
        {
            get { return this.location; }
            set { this.location = value; }
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
            get{  return this.deliveredBy; }
            set{ this.deliveredBy = value;}
        }

        #endregion



        #region Construtors

        public BookingModel()
        {

        }

        public BookingModel(int BookingID, string Customer, List<ItemLineModel> Items, List<DeviceModel> Devices, BuildingModel Location, DateTime PlannedBorrowDate, DateTime PlannedReturnDate, string DeliveredBy = null, string Notes = null)
        {
            this.bookingID = BookingID;
            this.customer = Customer;
            this.notes = Notes;
            this.items = Items;
            this.devices = Devices;
            this.location = Location;
            this.plannedBorrowDate = PlannedBorrowDate;
            this.plannedReturnDate = PlannedReturnDate;
            this.deliveredBy = DeliveredBy;
        }

        #endregion
    }
}
