using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class Tasks
    {
        private List<BookingModel> bookingsToBeDelivered;
        private List<BookingModel> bookingsToBeRetrieved;


        public List<BookingModel> BookingsToBeDelivered
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
        public List<BookingModel> BookingsToBeRetrieved
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

        public Tasks()
        {

        }
        public Tasks(List<BookingModel> BookingsToBeDelivered, List<BookingModel> BookingsToBeRetrieved)
        {
            this.bookingsToBeDelivered = BookingsToBeDelivered;
            this.bookingsToBeRetrieved = BookingsToBeRetrieved;
        }
    }
}
