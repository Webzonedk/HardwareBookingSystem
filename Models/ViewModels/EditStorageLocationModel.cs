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
        private List<string> deleteBuildings;
        private List<byte> deleteRoomNumbers;
        private string deleteRoom;
        private List<string> deleteShelfNames;
        private List<byte> deleteShelfLevels;
        private List<byte> deleteShelfSpots;
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

        public List<string> DeleteBuildings
        {
            get { return deleteBuildings; }
            set { deleteBuildings = value; }
        }

        public List<byte> DeleteRoomNumbers
        {
            get { return deleteRoomNumbers; }
            set { deleteRoomNumbers = value; }
        }

        public string DeleteRoom
        {
            get { return deleteRoom; }
            set { deleteRoom = value; }
        }

        public List<string> DeleteShelfNames
        {
            get { return deleteShelfNames; }
            set { deleteShelfNames = value; }
        }

        public List<byte> DeleteShelfLevels
        {
            get { return deleteShelfLevels; }
            set { deleteShelfLevels = value; }
        }

        public List<byte> DeleteShelfSpots
        {
            get { return deleteShelfSpots; }
            set { deleteShelfSpots = value; }
        }


        #endregion






        #region Constructors
        public EditStorageLocationModel()
        {
            storageLocations = new List<StorageLocationModel>();
            buildings = new List<string>();
            roomNumbers = new List<byte>();
            shelfNames = new List<string>();
            shelfLevels = new List<byte>();
            shelfSpots = new List<byte>();
            deleteBuildings = new List<string>();
            deleteRoomNumbers = new List<byte>();
            deleteShelfNames = new List<string>();
            deleteShelfLevels = new List<byte>();
            deleteShelfSpots = new List<byte>();
        }

        public EditStorageLocationModel(StorageLocationModel storageLocation, List<StorageLocationModel> storageLocations, List<string> buildings, List<byte> roomNumbers,
            List<string> shelfNames, List<byte> shelfLevels, List<byte> shelfSpots, byte filter,
            List<string>deleteBuildings, List<byte> deleteRoomNumbers, string deleteRoom, List<string> deleteShelfNames, List<byte> deleteShelfLevels, List<byte> deleteShelfSpots)
        {
            this.storageLocation = storageLocation;
            this.storageLocations = storageLocations;
            this.buildings = buildings;
            this.roomNumbers = roomNumbers;
            this.shelfNames = shelfNames;
            this.shelfLevels = shelfLevels;
            this.shelfSpots = shelfSpots;
            this.filter = filter;
            this.deleteBuildings = deleteBuildings;
            this.deleteRoomNumbers = deleteRoomNumbers;
            this.deleteRoom = deleteRoom;
            this.deleteShelfNames = deleteShelfNames;
            this.deleteShelfLevels = deleteShelfLevels;
            this.deleteShelfSpots = deleteShelfSpots;
        }
        #endregion
    }
}
