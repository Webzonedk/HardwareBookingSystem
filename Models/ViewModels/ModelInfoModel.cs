using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class ModelInfoModel
    {
        #region fields
        private DeviceModel device;
        private List<DeviceModel> borrowedDevices;
        private List<string> categoryNames;
        private List<string> inventoryStatuses;
        private string category;
        private byte filter;
        private string searchName;
        private string deviceStatus;
        private byte inStock;
        #endregion




        #region Properties

        public DeviceModel Device
        {
            get { return device; }
            set { device = value; }
        }


        public List<DeviceModel> BorrowedDevices
        {
            get { return borrowedDevices; }
            set { borrowedDevices = value; }
        }

        public List<string> Categories
        {
            get { return categoryNames; }
            set { categoryNames = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public byte Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; }
        }


        public byte InStock
        {
            get { return inStock; }
            set { inStock = value; }
        }

        public string DeviceStatus
        {
            get { return deviceStatus; }
            set { deviceStatus = value; }
        }

        public List<string> InventoryStatuses
        {
            get { return inventoryStatuses; }
            set { inventoryStatuses = value; }
        }

        #endregion



        #region Constructors
        public ModelInfoModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            borrowedDevices = new List<DeviceModel>();
            inventoryStatuses = new List<string>();
            this.deviceStatus = "Alle";
        }

        public ModelInfoModel(DeviceModel device, List<DeviceModel> borrowedDevices, List<string> categoryNames, string category, byte filter, string searchName, string deviceStatus, byte inStock, List<string> inventoryStatuses)
        {
            this.device = device;
            this.borrowedDevices = borrowedDevices;
            this.categoryNames = categoryNames;
            this.category = category;
            this.filter = filter;
            this.searchName = searchName;
            this.deviceStatus = deviceStatus;
            this.inStock = inStock;
            this.InventoryStatuses = inventoryStatuses;
        }
        #endregion
    }
}
