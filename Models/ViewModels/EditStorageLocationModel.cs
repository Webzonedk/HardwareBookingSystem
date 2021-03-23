using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class EditStorageLocationModel
    {
        #region fields
        private StorageLocationModel storageLocation;
        private List<StorageLocationModel> storageLocations;
        private List<string> buildings;
        private List<byte> roomNumbers;
        private List<string> shelfNames;
        private List<byte> shelfLevels;
        private List<byte> shelfSpots;
        private byte filter;
        #endregion







        #region Properties

        public StorageLocationModel StorageLocation
        {
            get { return storageLocation; }
            set { storageLocation = value; }
        }


        public List<StorageLocationModel> StorageLocations
        {
            get { return storageLocations; }
            set { storageLocations = value; }
        }


        public List<string> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }

        public List<byte> RoomNumbers
        {
            get { return roomNumbers; }
            set { roomNumbers = value; }
        }
        #endregion

        public List<string> ShelfNames
        {
            get { return shelfNames; }
            set { shelfNames = value; }
        }

        public List<byte> ShelfLevels
        {
            get { return shelfLevels; }
            set { shelfLevels = value; }
        }

        public List<byte> ShelfSpots
        {
            get { return shelfSpots; }
            set { shelfSpots = value; }
        }


        public byte Filter
        {
            get { return filter; }
            set { filter = value; }
        }



        #region Constructors
        public EditStorageLocationModel()
        {
            storageLocations = new List<StorageLocationModel>();
            buildings = new List<string>();
            roomNumbers = new List<byte>();
            shelfNames = new List<string>();
            shelfLevels = new List<byte>();
            shelfSpots = new List<byte>();
        }

        public EditStorageLocationModel(StorageLocationModel storageLocation, List<StorageLocationModel> storageLocations, List<string> buildings, List<byte> roomNumbers, List<string> shelfNames, List<byte> shelfLevels, List<byte> shelfSpots, byte filter)
        {
            this.storageLocation = storageLocation;
            this.storageLocations = storageLocations;
            this.buildings = buildings;
            this.roomNumbers = roomNumbers;
            this.shelfNames = shelfNames;
            this.shelfLevels = shelfLevels;
            this.shelfSpots = shelfSpots;
            this.filter = filter;
        }
        #endregion
    }
}
