using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HUS_project.Models
{
    public class Booking
    {
        #region Fields
        private User customer;
        private User deliveredBy;
        private User returnedBy;
        private List<Device> devices;
        private List<Device> logs;
        private string building;
        private string roomNumber;
        private DateTime dateBorrowed;
        private DateTime dateReturned;
        #endregion

        #region Properties
        public User Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
            }
        }
        public User DeliveredBy
        {
            get
            {
                return deliveredBy;
            }
            set
            {
                deliveredBy = value;
            }
        }
        public User ReturnedBy
        {
            get
            {
                return returnedBy;
            }
            set
            {
                returnedBy = value;
            }
        }
        public List<Device> Devices
        {
            get
            {
                return devices;
            }
            set
            {
                devices = value;
            }
        }
        public List<Device> Logs
        {
            get
            {
                return logs;
            }
            set
            {
                logs = value;
            }
        }
        public string Building
        {
            get
            {
                return building;
            }
            set
            {
                building = value;
            }
        }
        public string RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                roomNumber = value;
            }
        }
        public DateTime DateBorrowed
        {
            get
            {
                return dateBorrowed;
            }
            set
            {
                dateBorrowed = value;
            }
        }
        public DateTime DateReturned
        {
            get
            {
                return dateReturned;
            }
            set
            {
                dateReturned = value;
            }
        }
        #endregion
    }
}
