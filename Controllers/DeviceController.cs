using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models.ViewModels;
using HUS_project.Models;
using HUS_project.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace HUS_project.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IConfiguration configuration;


        // constructor of homecontroller
        public DeviceController(IConfiguration config)
        {
            this.configuration = config;
        }





        //get model name and category names before returning to view
        public IActionResult CreateDevice()
        {
            //get data from database
            DBManagerShared sharedDBManager = new DBManagerShared(configuration);
            List<string> categories = sharedDBManager.GetCategories();
            List<string> modelNames = sharedDBManager.GetModelNames();


            //create view model
            CreateDeviceModel deviceData = new CreateDeviceModel();
            deviceData.Categories = categories;
            deviceData.ModelNames = modelNames;
            deviceData.Device = new DeviceModel();

            //check if image exists in folder, based on model ID

            return View(deviceData);
        }

        [HttpPost]
        //Add Device to Database
        public IActionResult AddDeviceToDB(CreateDeviceModel deviceData)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            //Add device to database
            DeviceModel data = deviceData.Device;
            data.ChangedBy = HttpContext.Session.GetString("uniLogin");
            int deviceID = dbManager.CreateDevice(data);
            int modelID = dbsharedManager.GetModelID(data.Model.ModelName);

            //return device info to Edit view
            data = dbManager.GetDeviceInfoWithLocation(deviceID);
            List<DeviceModel> logs = GetDeviceLogs(deviceID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();

            //set image path
            string imagepath = deviceData.Image;
            string _webroot = (string)AppDomain.CurrentDomain.GetData("webRootPath");

            //check if image string is base64
            if (TryGetFromBase64String(deviceData.Image))
            {
                //convert image source to byte array
                string sourceimage = deviceData.Image;
                string base64 = sourceimage.Substring(sourceimage.IndexOf(',') + 1);
                byte[] datastream = Convert.FromBase64String(base64);

                //test new upload method
                string filenameOut = $"Capture_{modelID}.png";
                ImageModel imageM = new ImageModel(datastream, modelID, filenameOut);
                int success = dbManager.UploadImage(imageM);

                //set image source if successful upload
                if (success > 0)
                {
                    deviceData.Image = string.Format("data:image/png;base64,{0}", base64);
                }

                ////convert byte array to image file
                //using (MemoryStream m = new MemoryStream(datastream))
                //{
                //    using (Image image = Image.FromStream(m))
                //    {
                //        string root = (string)AppDomain.CurrentDomain.GetData("webRootPath");
                //        string webroot = root + "\\DeviceContent\\";

                //        string filename = $"Capture_{modelID}.png";
                //        if (Directory.Exists(webroot))
                //        {
                //            // save image to directory
                //            image.Save(webroot + filename, ImageFormat.Png);
                //            m.Dispose();
                //            image.Dispose();
                //            datastream = null;

                //            //get filename
                //            deviceData.Image = filename;
                //            Debug.WriteLine("image saved");
                //        }

                //    }

                //}
            }
            else
            {
                //check if image exists
                if (!CheckExistingModelNames(deviceData.Image))
                {
                   // string filepath = "missing_image.png";
                    deviceData.Image = null;
                }


            }




            //set return data
            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Shelf = new string($"{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");
            editdata.ImagePath = deviceData.Image;
            //  editdata.ImagePath = source;


            return View("EditView", editdata);
        }

        //check if image exists
        public IActionResult CheckImage(CreateDeviceModel deviceData)
        {
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            deviceData.Categories = categories;
            deviceData.ModelNames = modelNames;

            //check if image exists, if modelName is supplied
            if (deviceData.Device.Model.ModelName != null)
            {
                bool match = false;
                for (int i = 0; i < deviceData.ModelNames.Count; i++)
                {
                    if (deviceData.ModelNames[i] == deviceData.Device.Model.ModelName)
                    {
                        match = true;
                        break;
                    }
                }

                //check if image path exists
                if (match)
                {

                    int modelID = dbsharedManager.GetModelID(deviceData.Device.Model.ModelName);

                    ImageModel im = dbsharedManager.DownloadImage(modelID);
                    if(im.ImageData != null)
                    {
                        string newbase64 = Convert.ToBase64String(im.ImageData);
                        string source = string.Format("data:image/png;base64,{0}", newbase64);
                        deviceData.Image = source;
                    }

                  /*  string filename = $"Capture_{modelID}.png";
                    string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
                    if (System.IO.File.Exists(imagepath))
                    {
                        deviceData.Image = filename;
                    }*/
                }



            }

            return View("CreateDevice", deviceData);
        }


        //getting data from database & return model to view
        public IActionResult EditView(string submit)
        {

            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            //return device info to Edit view
            int ID = int.Parse(submit);
            DeviceModel data = new DeviceModel();

            data = dbManager.GetDeviceInfoWithLocation(ID);
            List<DeviceModel> logs = GetDeviceLogs(ID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            EditDeviceModel storagelocation = dbManager.GetStorageLocations(null);
            int modelID = dbsharedManager.GetModelID(data.Model.ModelName);

            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Shelf = new string($"{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");

            //test download image
            ImageModel im = dbsharedManager.DownloadImage(modelID);

            //set image path if
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                editdata.ImagePath = source;
            }



            //check if image exists
            //string filename = $"Capture_{modelID}.png";
            //string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
            //if (System.IO.File.Exists(imagepath))
            //{
            //    editdata.ImagePath = filename;
            //}


            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            editdata.Rooms = storagelocation.Rooms;

            //test to get all rooms and shelves
            storagelocation = dbManager.GetStorageLocations(editdata);
            editdata.Rooms = storagelocation.Rooms;
            editdata.Shelfs = storagelocation.Shelfs;


            return View(editdata);
        }

        // gets device location and returns to Edit view
        [HttpPost]
        public IActionResult GetLocations(EditDeviceModel data)
        {
            EditDeviceModel newdata = GetNewLocation(data);
            return View("EditView", newdata);
        }


        [HttpPost]
        //saves new location on device to database
        public IActionResult EditLocation(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared shared = new DBManagerShared(configuration);
            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");

            //prep data for database
            string[] splittedRoom = data.Room.Split('.');
            string[] splittedShelf = data.Shelf.Split('.');

            //set data models
            BuildingModel building = new BuildingModel(splittedRoom[0], splittedRoom[1]);
            StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[0], splittedShelf[1], splittedShelf[2], building);
            data.Device.Location = storageLocation;
            data.Device.Notes = "Placering redigeret";

            //send data to database
            data = dbManager.EditDeviceLocation(data);

            List<DeviceModel> logs = dbManager.GetDeviceLogs(data.Device.DeviceID);
            data.Logs = logs;

            //save Device name & other important things
            //send data to database
            int success = dbManager.EditDevice(data);
            int modelID = shared.GetModelID(data.Device.Model.ModelName);

            //check if image exists
            ImageModel im = shared.DownloadImage(modelID);
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                data.ImagePath = source;
            }

            //set message to be shown in view
            if (success > 0)
            {
                ViewBag.Location = "Placering Gemt";
            }
            else
            {
                ViewBag.Location = "Placering ikke Gemt";
            }

            return View("EditView", data);
        }

        [HttpPost]
        //saves all edits on device to database
        public IActionResult EditDevice(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            EditDeviceModel storagelocation = dbManager.GetStorageLocations(null);

            //check categories
            bool Category_validated = checkUserInput(data.Categories, data.Device.Model.Category.Category);

            //check modelNames
            bool ModelName_validated = checkUserInput(data.ModelNames, data.Device.Model.ModelName);

            //check BuildingNames
            bool Building_validated = checkUserInput(storagelocation.Rooms, data.Room);

            //check RoomNames
            bool Room_validated = checkUserInput(storagelocation.Shelfs, data.Shelf);

            if (Category_validated && ModelName_validated && Building_validated && Room_validated)
            {
                Debug.WriteLine("success");
            }
            else
            {
                //set view bags
                if (!Category_validated)
                {
                    ViewBag.Error = "indtast en gyldig kategori";
                }
                else if (!ModelName_validated)
                {
                    ViewBag.Error = "indtast et gyldigt model navn";
                }
                else if (!Building_validated)
                {
                    ViewBag.LocationError = "indtast en gyldig lokation";
                }
                else if (!Room_validated)
                {
                    ViewBag.LocationError = "indtast en gyldig hylde";
                }

                return View("EditView", data);
            }


            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");
            data.Device.Notes = "Enhed redigeret";

            #region saving new location
            //prep data for database
            string[] splittedRoom = data.Room.Split('.');
            string[] splittedShelf = data.Shelf.Split('.');

            //set data models
            BuildingModel building = new BuildingModel(splittedRoom[0], splittedRoom[1]);
            StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[0], splittedShelf[1], splittedShelf[2], building);
            data.Device.Location = storageLocation;
            //data.Device.Notes = "Placering redigeret";

            //send data to database
            data = dbManager.EditDeviceLocation(data);
            #endregion

            //get the logs again
            List<DeviceModel> logs = dbManager.GetDeviceLogs(data.Device.DeviceID);
            data.Logs = logs;

            //send data to database
            int success = dbManager.EditDevice(data);

            //set message to be shown in view
            if (success > 0)
            {
                ViewBag.edit = "Enhed Gemt";
            }
            else
            {
                ViewBag.edit = "Enhed ikke Gemt";
            }

            return View("EditView", data);
        }

        //Deactivate Device
        public IActionResult DeleteDevice(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");

            data.Device.Status = 0;

            //change status of device to deactivated
            int success = dbManager.EditDevice(data);
            if (success > 0)
            {
                ViewBag.Delete = "Enhed slettet";
            }
            else
            {
                ViewBag.Delete = "Enhed er i brug";
            }

            //create blank model
            data = new EditDeviceModel();
            DeviceModel device = new DeviceModel();
            data.Device = device;

            // clear model
            ModelState.Clear();

            return View("EditView", data);
        }
        public IActionResult Inventory(ModelInfoModel infoList)
        {
            //generate an instance of the database manager
            DBManagerDevice DBDManager = new DBManagerDevice(configuration);

            //set dummy data to database
            infoList.SearchName = "L";

            infoList.Category = null;
            infoList.InStock = 0;

            //get data from the manager
            infoList = DBDManager.GetDeviceInventory(infoList);

            //test image validation
            CheckImageValidations();

            //send data to the manager

            //var combinedLists = infoList.BorrowedDevices.Zip(infoList.InventoryStatuses, (b, i) => new { device = b, status = i });
            //foreach (var item in combinedLists)
            //{
            //    Debug.WriteLine("device ID: " +item.device.DeviceID + " status: " + item.status);
            //}

            return View(infoList);
        }

        //opens view for scanning of QR codes
        public IActionResult ScanLocation(EditDeviceModel data)
        {
            data.Room = "";
            return View(data);
        }

        //return new data to Editview
        [HttpPost]
        public IActionResult ReturnScanData(EditDeviceModel data)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);

            string[] splittedData = data.Room.Split('-');

            //create dummy data
            splittedData[1] = "K.1.A.0.4";

            //get data from splitted strings
            int id = int.Parse(splittedData[0]);
            string[] location = splittedData[1].Split('.');

            //fill model with data
            DeviceModel device = dbManager.GetDeviceInfoWithLocation(id);
            EditDeviceModel newdata = new EditDeviceModel();
            newdata.Device = device;
            newdata.Room = $"{location[0]}.{location[1]}";
            newdata.Shelf = $"{location[2]}.{location[3]}.{location[4]}";
            newdata = GetNewLocation(newdata);


            return View("EditView", newdata);

        }

        //gather data to be sent to QR Generator
        [HttpPost]
        public IActionResult PrepForQR(EditDeviceModel input)
        {
            //add single string to list
            List<string> output = new List<string>();
            string data = $"Dev-{input.Device.DeviceID}-{input.Device.SerialNumber}";
            output.Add(data);
            output.Add(data);
            output.Add(data);
            output.Add(data);
            output.Add(data);
            output.Add(data);
            //redirect to method
            return SendToQRController(output);
        }

        //send data to QR controller
        [HttpPost]
        public IActionResult SendToQRController(List<string> data)
        {


            //debugging test of multiple strings
            for (int i = 0; i < data.Count; i++)
            {
                string testdata = $"{data[i]}";
                data[i] = testdata;
            }


            TempData["QRData"] = data.ToArray();

            return RedirectToAction("PrintQR", "QRCode");
        }


        #region Helper methods
        //returns list of device logs
        private List<DeviceModel> GetDeviceLogs(int id)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            List<DeviceModel> data = dbManager.GetDeviceLogs(id);
            return data;
        }

        //returns model with new location
        private EditDeviceModel GetNewLocation(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared shared = new DBManagerShared(configuration);

            //get the logs back again
            List<DeviceModel> logs = dbManager.GetDeviceLogs(data.Device.DeviceID);
            int modelID = shared.GetModelID(data.Device.Model.ModelName);
            data.Logs = logs;
            EditDeviceModel newdata = data;

            //check if image exists & set image path
            ImageModel im = shared.DownloadImage(modelID);
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                newdata.ImagePath = source;
            }





            //fetch storage locations if user has typed a valid room
            if (data.Room != null)
            {
                //prep data for database
                string[] splittedRoom = data.Room.Split('.');

                //prep data model
                EditDeviceModel editData = new EditDeviceModel();
                DeviceModel device = new DeviceModel();
                BuildingModel building = new BuildingModel(splittedRoom[0], splittedRoom[1]);
                StorageLocationModel storageLocation = new StorageLocationModel();
                storageLocation.Location = building;
                device.Location = storageLocation;
                editData.Device = device;

                //get storagelocations
                EditDeviceModel locations = dbManager.GetStorageLocations(editData);

                newdata.Shelfs = locations.Shelfs;

                //fill out shelf data if present
                if (data.Shelf != null)
                {
                    newdata.Shelf = data.Shelf;
                }
                else
                {
                    newdata.Shelf = null;
                }



            }
            //return the same data without having selected anything
            else
            {
                EditDeviceModel storagelocation = dbManager.GetStorageLocations(null);
                newdata.Rooms = storagelocation.Rooms;
                newdata.Shelf = null;
            }




            // clear model
            ModelState.Clear();

            return newdata;
        }

        //check inputvalidation for dropdown
        private bool checkUserInput(List<string> dropdown, string userInput)
        {

            //check dropdown vs unserInput
            bool inputValidated = false;
            foreach (var element in dropdown)
            {
                if (element == userInput)
                {
                    inputValidated = true;
                    break;
                }

            }



            if (inputValidated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //checks if string is base64
        private bool TryGetFromBase64String(string input)
        {
            string base64 = input.Substring(input.IndexOf(',') + 1);

            try
            {
                byte[] output = Convert.FromBase64String(base64);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckExistingModelNames(string image)
        {
            string[] paths = Directory.GetFiles(AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\");
            // int numimages = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\"
            for (int i = 0; i < paths.Length; i++)
            {
                Debug.WriteLine(paths[i]);
                if (image == paths[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckImageValidations()
        {
            //  DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared sharedDBManager = new DBManagerShared(configuration);
            List<string> modelNames = sharedDBManager.GetModelNames();

            //get list of model IDs
            List<int> modelIDs = new List<int>();
            for (int i = 0; i < modelNames.Count; i++)
            {
                modelIDs.Add(sharedDBManager.GetModelID(modelNames[i]));
            }

            //get list of file names

            string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\";
            string[] imagepaths = Directory.GetFiles(imagepath);

            for (int j = 0; j < imagepaths.Length; j++)
            {
                bool match = false;
                string[] subs = imagepaths[j].Split('\\');
                string fileName = subs[subs.Length - 1];

                for (int k = 0; k < modelIDs.Count; k++)
                {
                    string comparer = $"Capture_{modelIDs[k]}.png";
                    Debug.WriteLine($"comparing {comparer} width {fileName}");
                    if (fileName == comparer)
                    {
                        match = true;
                        break;
                    }

                }

                if (!match)
                {
                    if (fileName != "missing_image")
                    {

                        Debug.WriteLine($"model does not exists: {fileName}");
                    }
                }
            }


        }

        #endregion


    }
}
