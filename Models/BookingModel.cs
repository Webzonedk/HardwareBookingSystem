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
        private List<BookedDeviceModel> devices;
        private BuildingModel location;
        private DateTime plannedBorrowDate;
        private DateTime plannedReturnDate;
        private string deliveredBy;
        private byte bookingStatus;

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

        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        public List<ItemLineModel> Items
        {
            get { return items; }
            set { items = value; }
        }

        public List<BookedDeviceModel> Devices
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


        public byte BookingStatus
        {
            get { return bookingStatus; }
            set { bookingStatus = value; }
        }

        #endregion



        #region Construtors

        public BookingModel()
        {

        }

        public BookingModel(int BookingID, string Customer, List<ItemLineModel> Items, List<BookedDeviceModel> Devices, BuildingModel Location, DateTime PlannedBorrowDate, DateTime PlannedReturnDate, byte BookingStatus, string DeliveredBy = null, string Notes = null)
        {
            this.bookingID = BookingID;
            this.customer = Customer;
            this.notes = Notes;
            this.items = Items;
            this.devices = Devices;
            this.location = Location;
            this.plannedBorrowDate = PlannedBorrowDate;
            this.plannedReturnDate = PlannedReturnDate;
            this.bookingStatus = BookingStatus;
            this.deliveredBy = DeliveredBy;
        }

        #endregion
    }
}
