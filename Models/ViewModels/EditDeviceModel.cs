﻿using System;
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
        private List<string> modelNames;
        private List<string> categoryNames;
        private List<string> rooms;
        private List<string> locations;
        private List<DeviceModel> logs;
        private List<DeviceModel> locationLogs;
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

        #endregion

        #region Constructors
        public EditDeviceModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            modelNames = new List<string>();
            rooms = new List<string>();
            locations = new List<string>();
            logs = new List<DeviceModel>();
            locationLogs = new List<DeviceModel>();
        }

        public EditDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories, List<string> rooms,List<string> locations,List<DeviceModel> logs, List<DeviceModel> locationlogs)
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
