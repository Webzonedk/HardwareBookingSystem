using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class StorageLocationModel
    {
        #region Fields

        private int locationID;
        private string shelfName;
        private string shelfLevel;
        private string shelfSpot;
        private BuildingModel location;

        #endregion



        #region Properties



        public int LocationID
        {
            get { return locationID; }
            set { locationID = value; }
        }

        public string ShelfName
        {
            get { return shelfName; }
            set { shelfName = value; }
        }


        public string ShelfLevel
        {
            get { return shelfLevel; }
            set { shelfLevel = value; }
        }


        public string ShelfSpot
        {
            get { return shelfSpot; }
            set { shelfSpot = value; }
        }


        public BuildingModel Location
        {
            get { return location; }
            set { location = value; }
        }

        #endregion



        #region Constructors

        public StorageLocationModel()
        {

        }
        
        public StorageLocationModel(string shelfName, string shelfLevel, string shelfSpot, BuildingModel location)
        {
            this.shelfName = shelfName;
            this.shelfLevel = shelfLevel;
            this.shelfSpot = shelfSpot;
            this.location = location;
        }

        #endregion


    }
}
