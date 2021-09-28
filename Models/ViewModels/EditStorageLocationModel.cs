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
        private List<string> roomNumbers;
        private List<string> shelfNames;
        private List<string> shelfLevels;
        private List<string> shelfSpots;
        private byte filter;
        private List<string> deleteBuildings;
        private List<string> deleteRoomNumbers;
        private List<string> deleteShelfNames;
        private List<string> deleteShelfLevels;
        private List<string> deleteShelfSpots;
        private string deleteBuilding;
        private string deleteRoomNumber;
        private string deleteRoom;
        private string deleteShelfName;
        private string deleteShelfLevel;
        private string deleteShelfSpot;
        private string hiddenFieldID;

        public string HiddenFieldID
        {
            get { return hiddenFieldID; }
            set { hiddenFieldID = value; }
        }

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

        public List<string> RoomNumbers
        {
            get { return roomNumbers; }
            set { roomNumbers = value; }
        }

        public List<string> ShelfNames
        {
            get { return shelfNames; }
            set { shelfNames = value; }
        }

        public List<string> ShelfLevels
        {
            get { return shelfLevels; }
            set { shelfLevels = value; }
        }

        public List<string> ShelfSpots
        {
            get { return shelfSpots; }
            set { shelfSpots = value; }
        }

        public byte Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        public List<string> DeleteBuildings
        {
            get { return deleteBuildings; }
            set { deleteBuildings = value; }
        }


        public List<string> DeleteRoomNumbers
        {
            get { return deleteRoomNumbers; }
            set { deleteRoomNumbers = value; }
        }


        public List<string> DeleteShelfNames
        {
            get { return deleteShelfNames; }
            set { deleteShelfNames = value; }
        }


        public List<string> DeleteShelfLevels
        {
            get { return deleteShelfLevels; }
            set { deleteShelfLevels = value; }
        }


        public List<string> DeleteShelfSpots
        {
            get { return deleteShelfSpots; }
            set { deleteShelfSpots = value; }
        }


        public string DeleteBuilding
        {
            get { return deleteBuilding; }
            set { deleteBuilding = value; }
        }

        public string DeleteRoomNumber
        {
            get { return deleteRoomNumber; }
            set { deleteRoomNumber = value; }
        }

        public string DeleteRoom
        {
            get { return deleteRoom; }
            set { deleteRoom = value; }
        }

        public string DeleteShelfName
        {
            get { return deleteShelfName; }
            set { deleteShelfName = value; }
        }

        public string DeleteShelfLevel
        {
            get { return deleteShelfLevel; }
            set { deleteShelfLevel = value; }
        }

        public string DeleteShelfSpot
        {
            get { return deleteShelfSpot; }
            set { deleteShelfSpot = value; }
        }

    
        #endregion






        #region Constructors
        public EditStorageLocationModel()
        {
            storageLocations = new List<StorageLocationModel>();
            buildings = new List<string>();
            roomNumbers = new List<string>();
            shelfNames = new List<string>();
            shelfLevels = new List<string>();
            shelfSpots = new List<string>();
            deleteBuildings = new List<string>();
            deleteRoomNumbers = new List<string>();
            deleteShelfNames = new List<string>();
            deleteShelfLevels = new List<string>();
            deleteShelfSpots = new List<string>();
        }

        public EditStorageLocationModel(StorageLocationModel storageLocation, List<StorageLocationModel> storageLocations, List<string> buildings, List<string> roomNumbers,
            List<string> shelfNames, List<string> shelfLevels, List<string> shelfSpots, byte filter,
            string deleteBuilding, string deleteRoomNumber, string deleteRoom, string deleteShelfName, string deleteShelfLevel, string deleteShelfSpot)
        {
            this.storageLocation = storageLocation;
            this.storageLocations = storageLocations;
            this.buildings = buildings;
            this.roomNumbers = roomNumbers;
            this.shelfNames = shelfNames;
            this.shelfLevels = shelfLevels;
            this.shelfSpots = shelfSpots;
            this.filter = filter;
            this.deleteBuilding = deleteBuilding;
            this.deleteRoomNumber = deleteRoomNumber;
            this.deleteRoom = deleteRoom;
            this.deleteShelfName = deleteShelfName;
            this.deleteShelfLevel = deleteShelfLevel;
            this.deleteShelfSpot = deleteShelfSpot;
        }
        #endregion
    }
}
