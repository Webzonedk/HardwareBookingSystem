using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class EditDeviceModel
    {
        #region fields
        private DeviceModel device;
        private List<string> modelNames;
        private List<string> categoryNames;
        private List<BuildingModel> rooms;
        private List<StorageLocationModel> locations;
        private List<DeviceModel> logs;
        private List<DeviceModel> locationLogs;
        #endregion

        #region Properties

        public DeviceModel Device
        {
            get { return device; }
            set { device = value; }
        }


        public List<string> ModelNames
        {
            get { return modelNames; }
            set { modelNames = value; }
        }


        public List<string> Categories
        {
            get { return categoryNames; }
            set { categoryNames = value; }
        }

        public List<BuildingModel> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        public List<StorageLocationModel> Locations
        {
            get { return locations; }
            set { locations = value; }
        }

        public List<DeviceModel> Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public List<DeviceModel> LocationLogs
        {
            get { return locationLogs; }
            set { locationLogs = value; }
        }

        #endregion

        #region Constructors
        public EditDeviceModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            modelNames = new List<string>();
            rooms = new List<BuildingModel>();
            locations = new List<StorageLocationModel>();
            logs = new List<DeviceModel>();
            locationLogs = new List<DeviceModel>();
        }

        public EditDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories, List<BuildingModel> rooms,List<StorageLocationModel> locations,List<DeviceModel> logs, List<DeviceModel> locationlogs)
        {
            this.device = device;
            this.modelNames = modelNames;
            this.categoryNames = categories;
            this.rooms = rooms;
            this.locations = locations;
            this.logs = logs;
            this.LocationLogs = locationlogs;
        }
        #endregion
    }
}
