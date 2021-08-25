using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class ImageModel
    {
        #region fields

        private byte[] imageData;
        private int modelID;
        private string fileName;

        //constructor
        public ImageModel()
        {

        }

        public ImageModel(byte[] imageData, int modelID, string fileName)
        {
            this.imageData = imageData;
            this.modelID = modelID;
            this.fileName = fileName;
        }

        #endregion

        #region Properties

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public int ModelID
        {
            get { return modelID; }
            set { modelID = value; }
        }
        public byte[] ImageData
        {
            get { return imageData; }
            set { imageData = value; }
        }
      
        #endregion










    }
}
