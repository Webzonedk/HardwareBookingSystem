using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models
{
    public class QRImageModel
    {
        private byte[] imageData;
        private string labelName;
        public byte[] ImageData { get => imageData; set => imageData = value; }
        public string LabelName { get => labelName; set => labelName = value; }

        public QRImageModel(byte[] imageData, string labelName)
        {
            this.ImageData = imageData;
            this.LabelName = labelName;
        }

    }
}
