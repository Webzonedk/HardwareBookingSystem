using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class BookedDevicesCRUModel
    {
        #region Variables
            // The Booking there are DeviceModels to be added/removed from.
            private BookingModel booking;

            // How many of the requested Models are currently in storage
            private List<ItemLineModel> modelsInStorage;

            // The StorageLocation of the requested modelName
            private Dictionary<string, StorageLocationModel> storageLocations;

        #endregion
        #region GetsNSets
            public BookingModel Booking
            {
                get { return this.booking; }
                set { this.booking = value; }
            }

            public List<ItemLineModel> ModelsInStorage
            {
                get { return this.modelsInStorage; }
                set { this.modelsInStorage = value; }
            }

            public Dictionary<string, StorageLocationModel> StorageLocations
            {
                get { return this.storageLocations; }
                set { this.storageLocations = value; }
            }


        #endregion
        #region Constructors
            public BookedDevicesCRUModel()
            {

            }
            public BookedDevicesCRUModel(BookingModel Booking, List<ItemLineModel> ModelsInStorage, Dictionary<string, StorageLocationModel> StorageLocations)
            {
                this.booking = Booking;
                this.modelsInStorage = ModelsInStorage;
                this.storageLocations = StorageLocations;
            }
        #endregion
    }
}
