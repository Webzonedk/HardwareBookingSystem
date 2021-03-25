using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class SortFilterModel
    {

        #region Fields
        private string headerTitle;
        private byte headerTitleValue;

        #endregion



        #region Properties
        public string HeaderTitle
        {
            get { return headerTitle; }
            set { headerTitle = value; }
        }



        public byte HeaderTitleValue
        {
            get { return headerTitleValue; }
            set { headerTitleValue = value; }
        }


        #endregion

        #region Constructors

        public SortFilterModel()
        {
        }
         public SortFilterModel(string headerTitle, byte headerTitleValue )
        {
            this.headerTitle = headerTitle;
            this.headerTitleValue = headerTitleValue;
        }

        #endregion




    }
}
