using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class CategoryModel
    {

        #region Fields
        
        private string category;

        #endregion


        #region Properties

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        #endregion



        #region Constructors

        public CategoryModel(string category)
        {
            this.category = category;
        }

        #endregion

    }
}
