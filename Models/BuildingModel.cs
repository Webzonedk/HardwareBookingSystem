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
        private byte roomNr;

        #endregion


        #region properties
        
        public string Building
        {
            get { return building; }
            set { building = value; }
        }


        public byte RoomNr
        {
            get { return roomNr; }
            set { roomNr = value; }
        }

        #endregion


        #region Constructors

        public BuildingModel()
        {

        }
              
        public BuildingModel(string building, byte roomNr)
        {
            this.building = building;
            this.roomNr = roomNr;
        }

        #endregion
    }

}
