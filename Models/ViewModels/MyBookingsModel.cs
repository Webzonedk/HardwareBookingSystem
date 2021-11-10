using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class MyBookingsModel
    {
        List<BookingModel> activeBookings;
        List<BookingModel> comingBookings;
        List<BookingModel> oldBookings;
        public List<BookingModel> ActiveBookings
        {
            get { return this.activeBookings; }
            set { this.activeBookings = value; }
        }
        public List<BookingModel> ComingBookings
        {
            get { return this.comingBookings; }
            set { this.comingBookings = value; }
        }
        public List<BookingModel> OldBookings
        {
            get { return this.oldBookings; }
            set { this.oldBookings = value; }
        }
        public MyBookingsModel()
        {
            
        }
    }
}
