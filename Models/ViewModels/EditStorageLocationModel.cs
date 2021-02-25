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
        private List<string> buildings;
        private List<byte> roomNumbers;
        private List<string> shelfNames;
        private List<byte> shelfLevels;
        private List<byte> shelfSpots;
        #endregion

       

    



        #region Properties

        public StorageLocationModel StorageLocation
        {
            get { return storageLocation; }
            set { storageLocation = value; }
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


        #region Constructors
        public EditStorageLocationModel()
        {
            buildings = new List<string>();
            roomNumbers = new List<byte>();
            shelfNames = new List<string>();
            shelfLevels = new List<byte>();
            shelfSpots = new List<byte>();
        }

        public EditStorageLocationModel(StorageLocationModel storageLocation, List<string> buildings, List<byte> roomNumbers, List<string> shelfNames, List<byte> shelfLevels, List<byte> shelfSpots)
        {
            this.storageLocation = storageLocation;
            this.buildings = buildings;
            this.roomNumbers = roomNumbers;
            this.shelfNames = shelfNames;
            this.shelfLevels = shelfLevels;
            this.shelfSpots = shelfSpots;
        }
        #endregion
    }
}
