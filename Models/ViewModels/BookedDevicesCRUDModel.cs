using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class BookedDevicesCRUDModel
    {
        #region Variables
            private BookingModel booking;
            private List<ItemLineModel> modelsInStorage;
            private Dictionary<ItemLineModel, StorageLocationModel> storageLocations;
        #endregion
        #region GetsNSets
            public BookingModel Booking
            {
                get { return booking; }
                set { booking = value; }
            }

            public List<ItemLineModel> ModelsInStorage
            {
                get { return modelsInStorage; }
                set { modelsInStorage = value; }
            }

            public Dictionary<ItemLineModel, StorageLocationModel> StorageLocations
            {
                get { return storageLocations; }
                set { this.storageLocations = value; }
            }
        #endregion
        #region Constructors
            public BookedDevicesCRUDModel()
            {

            }
            public BookedDevicesCRUDModel(BookingModel Booking, List<ItemLineModel> ModelsInStorage, Dictionary<ItemLineModel, StorageLocationModel> StorageLocations)
            {
                this.booking = Booking;
                this.modelsInStorage = ModelsInStorage;
                this.storageLocations = StorageLocations;
            }
        #endregion
    }
}
