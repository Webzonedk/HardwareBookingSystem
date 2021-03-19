using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class BookedDeviceModel
    {
        private DeviceModel device;
        private string returnedBy;
        private DateTime returnedDate;

        public DeviceModel Device
        {
            get { return this.device; }
            set { this.device = value; }
        }

        public string ReturnedBy
        {
            get { return this.returnedBy; }
            set { this.returnedBy = value; }
        }

        public DateTime ReturnedDate
        {
            get { return this.returnedDate; }
            set { this.returnedDate = value; }
        }

        public BookedDeviceModel()
        {

        }

        public BookedDeviceModel(DeviceModel Device, string ReturnedBy, DateTime ReturnedDate)
        {
            this.device = Device;
            this.returnedBy = ReturnedBy;
            this.returnedDate = ReturnedDate;
        }
    }
}
