using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using HUS_project.Models.ViewModels;
using HUS_project.Models;
using System.IO;

namespace HUS_project.Controllers
{
    public class QRCodeController : Controller
    {
        public IActionResult PrintQR()
        {
            string[] data = TempData["QRData"] as string[];
            QRModel qrCode = GenerateQRCode(data);

            return View(qrCode);
        }

        //generate QRcode & add it to model
        private QRModel GenerateQRCode(string[] inputData)
        {
            QRModel model = new QRModel();

            for (int i = 0; i < inputData.Length; i++)
            {
                QRCodeGenerator qrGen = new QRCodeGenerator();
                QRCodeData qrData = qrGen.CreateQrCode(inputData[i], QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrData);
                Bitmap qrImage = qrCode.GetGraphic(20);
                byte[] imgData = ConvertToBytes(qrImage);

                //add data to view model
                QRImageModel imgModel = new QRImageModel(imgData, inputData[i]);
                model.QRCode.Add(imgModel);

            }
            return model;
        }

        //convert bitmap to byte array
        private byte[] ConvertToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
