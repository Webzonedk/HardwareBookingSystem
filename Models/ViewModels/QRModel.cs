using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class QRModel
    {
        private List<QRImageModel> qrCode;
        public List<QRImageModel> QRCode { get => qrCode; set => qrCode = value; }

        public QRModel()
        {
            QRCode = new List<QRImageModel>();
            qrCode = QRCode;
        }

    }
}
