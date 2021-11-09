using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class DeviceQRScanningModel
    {
        int bookingID;
        string rawData;
        public int BookingID
        {
            get { return this.bookingID; }
            set { this.bookingID = value; }
        }
        public string RawData
        {
            get { return this.rawData; }
            set { this.rawData = value; }
        }
        public DeviceQRScanningModel()
        {

        }
        public DeviceQRScanningModel(int BookingID, string RawData)
        {
            this.bookingID = BookingID;
            this.rawData = RawData;
        }
    }
}
