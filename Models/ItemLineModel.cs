using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class ItemLineModel
    {
        #region Fields

        private string category;
        private int quantity;
        private ModelModel model;

        #endregion



        #region Properties

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public ModelModel Model
        {
            get { return model; }
            set { model = value; }
        }

        #endregion



        #region Constructors

        public ItemLineModel()
        {

        }

        public ItemLineModel(string category, int quantity, ModelModel model)
        {
            this.category = category;
            this.quantity = quantity;
            this.model = model;
        }

        #endregion
    }
}
