using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HUS_project.Models
{
    public class User
    {
        #region Fields
        private int bookingID;
        private string username;
        private string userclass;
        private string email;
        #endregion

        #region Properties
        public int BookingID
        {
            get
            {
                return bookingID;
            }
            set
            {
                bookingID = value;
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Userclass
        {
            get
            {
                return userclass;
            }
            set
            {
                userclass = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        #endregion
    }
}
