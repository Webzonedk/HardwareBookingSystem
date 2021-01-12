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
        private byte shelfNumber;
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


        public byte ShelfNumber
        {
            get { return shelfNumber; }
            set { shelfNumber = value; }
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
        
        public StorageLocationModel(string shelfName, byte shelfLevel, byte shelfNumber, BuildingModel location)
        {
            this.shelfName = shelfName;
            this.shelfLevel = shelfLevel;
            this.shelfNumber = shelfNumber;
            this.location = location;
        }

        #endregion


    }
}
