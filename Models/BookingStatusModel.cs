using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class BookingStatusModel
    {
        #region Fields
        private enum statusOptions { inactive, active };
        private StatusOptions status;
        #endregion


        #region Properties      

        public enum StatusOptions 
        {
        get { return statusOptions;
        }
        set { statusOptions = value; }
        }


#endregion




#region Constructors

public BookingStatusModel()
{ }

public BookingStatusModel(enum statusOptions, StatusOptions status)
{

}
#endregion



    }
}
