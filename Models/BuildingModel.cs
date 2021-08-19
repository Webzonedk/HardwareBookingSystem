using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HUS_project.Models
{
    public class BuildingModel
    {
        #region Fields

        private string building;
        private string roomNumber;

        #endregion


        #region properties
        
        public string Building
        {
            get { return building; }
            set { building = value; }
        }


        public string RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }

        #endregion


        #region Constructors

        public BuildingModel()
        {

        }
              
        public BuildingModel(string building, string roomNumber)
        {
            this.building = building;
            this.roomNumber = roomNumber;
        }

        #endregion
    }

}
