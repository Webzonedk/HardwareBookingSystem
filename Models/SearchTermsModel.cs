using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class SearchTermsModel
    {
        #region Fields
        private string category;
        private DateTime rentDate;
        private DateTime returnDate;
        private string deliveryLocation;
        private string searchPhrase;

        #endregion



        #region Properties

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public DateTime RentDate
        {
            get { return rentDate; }
            set { rentDate = value; }
        }

        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        public string DeliveryLocation
        {
            get { return deliveryLocation; }
            set { deliveryLocation = value; }
        }

        public string SearchPhrase
        {
            get { return searchPhrase; }
            set { searchPhrase = value; }
        }
        #endregion



        #region Constructors
        public SearchTermsModel()
        {

        }

        public SearchTermsModel(string category, DateTime rentDate, DateTime returnDate, string deliveryLocation, string searchPhrase)
        {
            this.category = category;
            this.rentDate = rentDate;
            this.returnDate = returnDate;
            this.deliveryLocation = deliveryLocation;
            this.searchPhrase = searchPhrase;
        }
        #endregion
    }
}
