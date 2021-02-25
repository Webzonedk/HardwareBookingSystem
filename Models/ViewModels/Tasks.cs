using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.ViewModels
{
    public class Tasks
    {
        private List<Models.BookingModel> bookingsToBeDelivered;
        private List<Models.BookingModel> bookingsToBeRetrieved;

        public List<Models.BookingModel> BookingsToBeDelivered
        {
            get
            {
                return this.bookingsToBeDelivered;
            }
            set
            {
                this.bookingsToBeDelivered = value;
            }
        }
        public List<Models.BookingModel> BookingsToBeRetrieved
        {
            get
            {
                return this.bookingsToBeRetrieved;
            }
            set
            {
                this.bookingsToBeRetrieved = value;
            }
        }

    }
}
