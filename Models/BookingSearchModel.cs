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
        private List<int> inStock;
        private List<int> notInstock;
        private List<DateTime> rentDate;
        private List<DateTime> returnDate;


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



        public List<int> InStock { get => inStock; set => inStock = value; }
        public List<int> NotInstock { get => notInstock; set => notInstock = value; }
        public List<DateTime> RentDate { get => rentDate; set => rentDate = value; }
        public List<DateTime> ReturnDate { get => returnDate; set => returnDate = value; }

        public BookingSearchModel()
        {
            inStock = new List<int>();
            notInstock = new List<int>();
            rentDate = new List<DateTime>();
            returnDate = new List<DateTime>();
        }



    }
}
