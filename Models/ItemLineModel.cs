using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class ItemLineModel
    {
        #region Fields

        private int quantity;
        private ModelModel model;

        #endregion



        #region Properties


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

        public ItemLineModel(int quantity, ModelModel model)
        {
            this.quantity = quantity;
            this.model = model;
        }

        #endregion
    }
}
