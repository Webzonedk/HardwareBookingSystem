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
        private List<string> categories;
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
            get { return categories; }
            set { categories = value; }
        }



        #endregion

        #region Constructors
        public EditDeviceModel()
        {

        }

        public EditDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories)
        {
            this.device = device;
            this.modelNames = modelNames;
            this.categories = categories;
        }
        #endregion
    }
}
