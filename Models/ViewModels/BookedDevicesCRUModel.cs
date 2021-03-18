using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class BookedDevicesCRUModel
    {
        #region Variables
            // The Booking there are BookedModels to be added/removed from.
            private BookingModel booking;

            // How many of the requested Models are currently in storage !!!!!!!!!!!!
            private List<ItemLineModel> modelsInStorage;

            // The StorageLocation of the requested Model.
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
            public BookedDevicesCRUModel()
            {

            }
            public BookedDevicesCRUModel(BookingModel Booking, List<ItemLineModel> ModelsInStorage, Dictionary<ItemLineModel, StorageLocationModel> StorageLocations)
            {
                this.booking = Booking;
                this.modelsInStorage = ModelsInStorage;
                this.storageLocations = StorageLocations;
            }
        #endregion
    }
}
