﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class DeviceModel
    {
        #region Fields

        private int deviceID;
        private string serialNumber;
        private ModelModel model;
        private byte status;
        private StorageLocationModel location;
        private string notes;
        private DateTime changeDate;
        private string changedBy;
        private string returnedBy;
        private DateTime dateReturned;
    
        #endregion



        #region Properties

        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        public ModelModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public byte Status
        {
            get { return status; }
            set { status = value; }
        }

        public StorageLocationModel Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public DateTime ChangeDate
        {
            get { return changeDate; }
            set { changeDate = value; }
        }

        public string ChangedBy
        {
            get { return changedBy; }
            set { changedBy = value; }
        }

        public string ReturnedBy
        {
            get { return returnedBy; }
            set { returnedBy = value; }
        }

        public DateTime DateReturned
        {
            get { return dateReturned; }
            set { dateReturned = value; }
        }

        #endregion


        #region Constructors

        public DeviceModel()
        {
           

        }


        public DeviceModel(int deviceID,string serialnr, ModelModel model, StorageLocationModel location, byte status, string notes,
            DateTime changeDate, string changedBy, string returnedBy, DateTime dateReturned)
        {
           

            //set  fields
            this.deviceID = deviceID;
            this.serialNumber = serialnr;
            this.model = model;
            this.status = status;
            this.location = location;
            this.notes = notes;
            this.changeDate = changeDate;
            this.changedBy = changedBy;
            this.returnedBy = returnedBy;
            this.dateReturned = dateReturned;
        }

        #endregion
        
    }
}
