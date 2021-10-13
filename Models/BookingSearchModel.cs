using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class BookingSearchModel
    {
        private int modelID;
        private string modelName;
        private string categoryName;
        private int inStock;
        private DateTime rentDate;
        private DateTime returnDate;



        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }
        public DateTime RentDate
        {
            get { return rentDate; }
            set { rentDate = value; }
        }


        public int InStock
        {
            get { return inStock; }
            set { inStock = value; }
        }


        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }


        public int ModelID
        {
            get { return modelID; }
            set { modelID = value; }
        }

        public BookingSearchModel()
        {

        }



    }
}
