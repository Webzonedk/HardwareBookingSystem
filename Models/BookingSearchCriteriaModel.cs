using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class BookingSearchCriteriaModel
    {
        private string category;
        private string searchName;
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


        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; }
        }


        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public BookingSearchCriteriaModel(string category, string searchName, DateTime rentDate, DateTime returnDate)
        {
            this.category = category;
            this.searchName = searchName;
            this.rentDate = rentDate;
            this.returnDate = returnDate;
        }
        

    }
}
