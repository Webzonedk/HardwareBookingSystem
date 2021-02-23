using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class LocationDropDownModel
    {
        #region fields
        private StorageLocationModel storageLocation;
        private List<BuildingModel> rooms;
        #endregion




        #region Properties

        public StorageLocationModel StorageLocation
        {
            get { return storageLocation; }
            set { storageLocation = value; }
        }


        public List<BuildingModel> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        #endregion



        #region Constructors
        public LocationDropDownModel()
        {

        }

        public LocationDropDownModel(StorageLocationModel storageLocation, List<BuildingModel> rooms)
        {
            this.storageLocation = storageLocation;
            this.rooms = rooms;
        }
        #endregion
    }
}
