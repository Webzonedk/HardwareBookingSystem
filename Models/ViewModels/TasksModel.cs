using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class TasksModel
    {
        private List<BookingModel> bookingsToBeRetrieved;
        private List<BookingModel> bookingsToBeDelivered;

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

        public TasksModel()
        {

        }
        public TasksModel(List<BookingModel> BookingsToBeRetrieved, List<BookingModel> BookingsToBeDelivered)
        {
            this.bookingsToBeRetrieved = BookingsToBeRetrieved;
            this.bookingsToBeDelivered = BookingsToBeDelivered;
        }
    }
}
