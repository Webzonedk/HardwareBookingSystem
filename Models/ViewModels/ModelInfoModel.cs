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
     
        #endregion



        #region Constructors
        public ModelInfoModel()
        {

        }

        public ModelInfoModel(DeviceModel device, List<DeviceModel> borrowedDevices)
        {
            this.device = device;
            this.borrowedDevices = borrowedDevices;
        }
        #endregion
    }
}
