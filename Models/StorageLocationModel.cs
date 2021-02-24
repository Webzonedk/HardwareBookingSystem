using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class StorageLocationModel
    {
        #region Fields

        private string shelfName;
        private byte shelfLevel;
        private byte shelfSpot;
        private BuildingModel location;

        #endregion



        #region Properties

        public string ShelfName
        {
            get { return shelfName; }
            set { shelfName = value; }
        }


        public byte ShelfLevel
        {
            get { return shelfLevel; }
            set { shelfLevel = value; }
        }


        public byte ShelfSpot
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
        
        public StorageLocationModel(string shelfName, byte shelfLevel, byte shelfSpot, BuildingModel location)
        {
            this.shelfName = shelfName;
            this.shelfLevel = shelfLevel;
            this.shelfSpot = shelfSpot;
            this.location = location;
        }

        #endregion


    }
}
