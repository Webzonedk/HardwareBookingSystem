using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class EditDeviceModel
    {
        #region fields
        private DeviceModel device;
        private string room;
        private string shelf;
        private string imagePath;
        private List<string> modelNames;
        private List<string> categoryNames;
        private List<string> rooms;
        private List<string> shelfs;
        private List<DeviceModel> logs;
        private List<DeviceModel> locationLogs;
        private int selectedLogs;

       

        #endregion

        #region Properties

        public DeviceModel Device
        {
            get { return device; }
            set { device = value; }
        }

        public string Room
        {
            get { return room; }
            set { room = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public string Shelf
        {
            get { return shelf; }
            set { shelf = value; }
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

        public List<string> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        public List<string> Shelfs
        {
            get { return shelfs; }
            set { shelfs = value; }
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
        public int SelectedLogs
        {
            get { return selectedLogs; }
            set { selectedLogs = value; }
        }

        #endregion

        #region Constructors
        public EditDeviceModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            modelNames = new List<string>();
            rooms = new List<string>();
            shelfs = new List<string>();
            logs = new List<DeviceModel>();
            locationLogs = new List<DeviceModel>();
        }

        public EditDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories, List<string> rooms,List<string> locations,List<DeviceModel> logs, List<DeviceModel> locationlogs,string _imagepath)
        {
            this.device = device;
            this.modelNames = modelNames;
            this.categoryNames = categories;
            this.rooms = rooms;
            this.shelfs = locations;
            this.logs = logs;
            this.LocationLogs = locationlogs;
            this.imagePath = _imagepath;
        }
        #endregion
    }
}
