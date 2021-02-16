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
        private List<DeviceModel> logs;
        private BuildingModel location;
        private DateTime dateBorrowed;
        private string deliveredBy;
        private SearchTermsModel searchTerms;
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

        public List<DeviceModel> Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public BuildingModel Location
        {
            get { return location; }
            set { location = value; }
        }

        public DateTime DateBorrowed
        {
            get { return dateBorrowed; }
            set {  dateBorrowed = value; }
        }

        public string DeliveredBy
        {
            get{  return deliveredBy; }
            set{ deliveredBy = value;}
        }

        public SearchTermsModel SearchTerms
        {
            get { return searchTerms; }
            set { searchTerms = value; }
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

        public BookingModel(int bookingID, string customer, List<ItemLineModel> items, List<DeviceModel> devices, List<DeviceModel> logs, BuildingModel location, DateTime dateBorrowed, string deliveredBy, SearchTermsModel searchTerms, int bookingStatus)
        {
            this.bookingID = bookingID;
            this.customer = customer;
            this.items = items;
            this.devices = devices;
            this.logs = logs;
            this.location = location;
            this.dateBorrowed = dateBorrowed;
            this.deliveredBy = deliveredBy;
            this.searchTerms = searchTerms;
            this.bookingStatus = bookingStatus;
        }

        #endregion
    }
}
