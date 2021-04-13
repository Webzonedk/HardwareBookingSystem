using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class CreateDeviceModel
    {
        #region fields
        private DeviceModel device;
        private List<string> modelNames;
        private List<string> categoryNames;
        private string image;
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

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        #endregion

        #region Constructors
        public CreateDeviceModel()
        {
            //initialize lists
            categoryNames = new List<string>();
            modelNames = new List<string>();
        }

        public CreateDeviceModel(DeviceModel device, List<string> modelNames, List<string> categories,string _image)
        {
            this.device = device;
            this.modelNames = modelNames;
            this.categoryNames = categories;
            this.image = _image;
        }
        #endregion
    }
}
