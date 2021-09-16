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
        private string location;
        private string imagePath;
        private List<string> modelNames;
        private List<string> categoryNames;
        private List<string> locations;
        private List<DeviceModel> logs;
        private List<DeviceModel> locationLogs;
        private int selectedLogs;
        private int feedback;
       

        #endregion

        #region Properties

        public DeviceModel Device
        {
            get { return device; }
            set { device = value; }
        }

        
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
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

       

        public List<string> Locations
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
        public int SelectedLogs
        {
            get { return selectedLogs; }
            set { selectedLogs = value; }
        }

        public int Feedback
        {
            get { return feedback; }
            set { feedback = value; }
        }

        #endregion

        #region Constructors
        public EditDeviceModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            modelNames = new List<string>();
            locations = new List<string>();
            logs = new List<DeviceModel>();
            locationLogs = new List<DeviceModel>();
        }

        public EditDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories,List<string> locations,List<DeviceModel> logs, List<DeviceModel> locationlogs,string _imagepath)
        {
            this.device = device;
            this.modelNames = modelNames;
            this.categoryNames = categories;
            this.locations = locations;
            this.logs = logs;
            this.LocationLogs = locationlogs;
            this.imagePath = _imagepath;
        }
        #endregion
    }
}
