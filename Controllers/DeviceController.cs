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
            int modelID = dbsharedManager.GetModelID(deviceData.Device.Model.ModelName);

            //check if image exists
            //  string filename = $"Capture_{modelID}.png";
            string imagepath = deviceData.Image;
            string _webroot = (string)AppDomain.CurrentDomain.GetData("webRootPath");
            if (!imagepath.Contains(_webroot))
            {
                //convet image source to byte array
                string sourceimage = deviceData.Image;
                string base64 = sourceimage.Substring(sourceimage.IndexOf(',') + 1);
                byte[] datastream = Convert.FromBase64String(base64);

                //convert byte array to image file
                using (MemoryStream m = new MemoryStream(datastream))
                {
                    using (Image image = Image.FromStream(m))
                    {
                        string root = (string)AppDomain.CurrentDomain.GetData("webRootPath");
                        string webroot = root + "\\DeviceContent";

                        string filename = "\\Capture.png";
                        if (Directory.Exists(webroot))
                        {
                            // save image to directory
                            image.Save(webroot + filename, ImageFormat.Png);
                            m.Dispose();
                            image.Dispose();
                            datastream = null;

                            //get filename
                            deviceData.Image = filename;
                        }

                    }

                }
            }

           
            //Add device to database
            DeviceModel data = deviceData.Device;
            data.ChangedBy = HttpContext.Session.GetString("uniLogin");
            int deviceID = dbManager.CreateDevice(data);

            //return device info to Edit view
            data = dbManager.GetDeviceInfoWithLocation(deviceID);
            List<DeviceModel> logs = dbManager.GetDeviceLogs(deviceID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            modelID = dbsharedManager.GetModelID(data.Model.ModelName);

            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Shelf = new string($"{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");
            editdata.ImagePath = $"Capture_{modelID}.png";

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
                int modelID = dbsharedManager.GetModelID(deviceData.Device.Model.ModelName);


                //check if image path exists
                string filename = $"Capture_{modelID}.png";
                string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
                if (System.IO.File.Exists(imagepath))
                {
                    deviceData.Image = filename;
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
            List<DeviceModel> logs = dbManager.GetDeviceLogs(ID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            EditDeviceModel storagelocation = dbManager.GetStorageLocations(null);
            int modelID = dbsharedManager.GetModelID(data.Model.ModelName);

            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Shelf = new string($"{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");

            //check if image exists
            string filename = $"Capture_{modelID}.png";
            string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
            if (System.IO.File.Exists(imagepath))
            {
                editdata.ImagePath = filename;
            }


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
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared shared = new DBManagerShared(configuration);

            //get the logs back again
            List<DeviceModel> logs = dbManager.GetDeviceLogs(data.Device.DeviceID);
            int modelID = shared.GetModelID(data.Device.Model.ModelName);
            data.Logs = logs;
            EditDeviceModel newdata = data;

            //check if image exists
            string filename = $"Capture_{modelID}.png";
            string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
            if (System.IO.File.Exists(imagepath))
            {
                newdata.ImagePath = filename;
            }

            //fetch storage locations if user has typed a valid room
            if (data.Room != null)
            {
                //prep data for database
                string[] splittedRoom = data.Room.Split('.');

                //prep data model
                EditDeviceModel editData = new EditDeviceModel();
                DeviceModel device = new DeviceModel();
                BuildingModel building = new BuildingModel(splittedRoom[0], Convert.ToByte(splittedRoom[1]));
                StorageLocationModel storageLocation = new StorageLocationModel();
                storageLocation.Location = building;
                device.Location = storageLocation;
                editData.Device = device;

                //get storagelocations
                EditDeviceModel locations = dbManager.GetStorageLocations(editData);
                
                newdata.Shelfs = locations.Shelfs;
                newdata.Shelf = null;

                
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
            BuildingModel building = new BuildingModel(splittedRoom[0], Convert.ToByte(splittedRoom[1]));
            StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[0], Convert.ToByte(splittedShelf[1]), Convert.ToByte(splittedShelf[2]), building);
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
            string filename = $"Capture_{modelID}.png";
            string imagepath = (string)AppDomain.CurrentDomain.GetData("webRootPath") + "\\DeviceContent\\" + filename;
            if (System.IO.File.Exists(imagepath))
            {
                data.ImagePath = filename;
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
            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");
            data.Device.Notes = "Enhed redigeret";

            #region saving new location
            //prep data for database
            string[] splittedRoom = data.Room.Split('.');
            string[] splittedShelf = data.Shelf.Split('.');

            //set data models
            BuildingModel building = new BuildingModel(splittedRoom[0], Convert.ToByte(splittedRoom[1]));
            StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[0], Convert.ToByte(splittedShelf[1]), Convert.ToByte(splittedShelf[2]), building);
            data.Device.Location = storageLocation;
            data.Device.Notes = "Placering redigeret";

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
            //data.Device.Notes = "Enhed redigeret";
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

            data = new EditDeviceModel();
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

            //send data to the manager

            //var combinedLists = infoList.BorrowedDevices.Zip(infoList.InventoryStatuses, (b, i) => new { device = b, status = i });
            //foreach (var item in combinedLists)
            //{
            //    Debug.WriteLine("device ID: " +item.device.DeviceID + " status: " + item.status);
            //}

            return View(infoList);
        }

        //public IActionResult CreateQRCode()
        //{

        //}


    }
}
