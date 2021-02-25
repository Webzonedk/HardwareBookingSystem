using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class InactiveLogsModel
    {
        #region fields
        
         private List<DeviceModel> inactiveDevices;
         private List<BuildingModel> inactiveBuildings;
        #endregion




        #region Properties

    


        public List<DeviceModel> InactiveDevices
        {
            get { return inactiveDevices; }
            set { inactiveDevices = value; }
        }

        public List<BuildingModel> InactiveBuildings

        {
            get { return inactiveBuildings; }
            set { inactiveBuildings = value; }
        }

        #endregion



        #region Constructors
        public InactiveLogsModel()
        {

        }

        public InactiveLogsModel(List<DeviceModel> inactiveDevices, List<BuildingModel> inactiveBuildings)
        {
            this.inactiveDevices = inactiveDevices;
            this.inactiveBuildings = inactiveBuildings;
        }
        #endregion
    }
}
