using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class ModelModel
    {

        #region Fields

        private string modelName;
        private string modelDescription;
        private CategoryModel category;

        #endregion



        #region Properties

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public string ModelDescription
        {
            get { return modelDescription; }
            set { modelDescription = value; }
        }

        public CategoryModel Category
        {
            get { return category; }
            set { category = value; }
        }

        #endregion



        #region Constructors

        public ModelModel()
        {

        }
         public ModelModel(string modelName, string modelDescription, CategoryModel category)
        {
            this.modelName = modelName;
            this.modelDescription = modelDescription;
            this.category = category;
        }

        #endregion


    }
}
