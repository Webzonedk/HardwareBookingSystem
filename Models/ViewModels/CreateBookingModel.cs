using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class CreateBookingModel
    {
        private List<BookingSearchModel> inventoryBooking;
        private List<ItemLineModel> itemLines;

        public List<ItemLineModel> ItemLines
        {
            get { return itemLines; }
            set { itemLines = value; }
        }


        public List<BookingSearchModel> InventoryBooking
        {
            get { return inventoryBooking; }
            set { inventoryBooking = value; }
        }

        public CreateBookingModel()
        {
            inventoryBooking = new List<BookingSearchModel>();
            itemLines = new List<ItemLineModel>();
        }
    }
}
