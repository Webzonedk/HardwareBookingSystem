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


            return View(deviceData);
        }

        //creates new category
        public IActionResult CreateCategory(CategoryModelAdmin_Model data)
        {
            // initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);
            int feedback = dbManager.CreateCategory(data.New_Category);


           
            data = GetAdminLists();

            return View("CategoryModelAdminView", data);
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



            //check if image string is base64
            if (TryGetFromBase64String(deviceData.Image))
            {
                //convert image source to byte array
                string sourceimage = deviceData.Image;
                string base64 = sourceimage.Substring(sourceimage.IndexOf(',') + 1);
                byte[] datastream = Convert.FromBase64String(base64);

                //upload image to DB
                string filenameOut = $"Capture_{modelID}.png";
                ImageModel imageM = new ImageModel(datastream, modelID, filenameOut);
                int success = dbManager.UploadImage(imageM);

                //set image source if successful upload
                if (success > 0)
                {
                    deviceData.Image = string.Format("data:image/png;base64,{0}", base64);
                }

            }
            else
            {
                //check if image exists
                //if (!CheckExistingModelNames(deviceData.Image))
                //{
                //    deviceData.Image = null;
                //}

                deviceData.Image = null;
            }

            //set return data
            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            //   editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Location = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}.{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");
            editdata.ImagePath = deviceData.Image;
            editdata.SelectedLogs = 10;
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
                    if (im.ImageData != null)
                    {
                        string newbase64 = Convert.ToBase64String(im.ImageData);
                        string source = string.Format("data:image/png;base64,{0}", newbase64);
                        deviceData.Image = source;
                    }

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
            EditDeviceModel storagelocation = dbManager.GetStorageLocations();
            int modelID = dbsharedManager.GetModelID(data.Model.ModelName);

            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            //   editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Location = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}.{data.Location.ShelfName}.{data.Location.ShelfLevel}.{data.Location.ShelfSpot}");

            //download image from DB
            ImageModel im = dbsharedManager.DownloadImage(modelID);

            //set image path if exists
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                editdata.ImagePath = source;
            }


            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            //  editdata.Rooms = storagelocation.Rooms;

            //get all rooms and shelves
            // storagelocation = dbManager.GetStorageLocations(editdata);
            //  editdata.Rooms = storagelocation.Rooms;
            editdata.Locations = storagelocation.Locations;
            editdata.SelectedLogs = 10;

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
        public IActionResult GetAllLogs(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            EditDeviceModel newdata = data;
            newdata.Logs = dbManager.GetAllDeviceLogs(data.Device.DeviceID);
            newdata.SelectedLogs = newdata.Logs.Count;
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
            string[] splittedShelf = data.Location.Split('.');
            bool inputValidated = false;



            if (splittedShelf.Length == 5)
            {
                inputValidated = true;
            }

            if (inputValidated)
            {
                //set data models
                BuildingModel building = new BuildingModel(splittedShelf[0], splittedShelf[1]);
                StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[2], splittedShelf[3], splittedShelf[4], building);
                data.Device.Location = storageLocation;
                data.Device.Notes = "Placering redigeret";
                bool locationValidated = dbManager.CheckLocation(storageLocation);

                if (locationValidated)
                {
                    //send data to database
                    data = dbManager.EditDeviceLocation(data);
                    dbManager.CreateLog(data);

                    //set message to be shown in view
                    if (data.Feedback > 0)
                    {
                        ViewBag.Location = "Placering Gemt";
                    }
                    else
                    {
                        ViewBag.LocationError = "Placering ikke Gemt";
                    }
                }
                else
                {
                    ViewBag.LocationError = "Placering er ikke korrekt indtastet";
                }

            }
            else
            {
                ViewBag.LocationError = "Placering er ikke korrekt indtastet";
            }

            List<DeviceModel> logs = dbManager.GetAllDeviceLogs(data.Device.DeviceID);
            EditDeviceModel locationList = dbManager.GetStorageLocations();
            data.Logs = logs;
            data.Locations = locationList.Locations;

            //get dropdwowns again
            data.Categories = shared.GetCategories();
            data.ModelNames = shared.GetModelNames();

            int modelID = shared.GetModelID(data.Device.Model.ModelName);

            //check if image exists
            ImageModel im = shared.DownloadImage(modelID);
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                data.ImagePath = source;
            }



            return View("EditView", data);
        }

        [HttpPost]
        //saves all edits on device to database
        public IActionResult EditDevice(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            EditDeviceModel storagelocation = dbManager.GetStorageLocations();



            //check RoomNames
            bool Room_validated = checkUserInput(storagelocation.Locations, data.Location);



            if (!Room_validated)
            {
                ViewBag.LocationError = "indtast en gyldig hylde";
                data.Locations = storagelocation.Locations;
                return View("EditView", data);
            }



            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");
            data.Device.Notes = "Enhed redigeret";


            #region saving new location
            //prep data for database
            string[] splittedShelf = data.Location.Split('.');

            //set data models
            BuildingModel building = new BuildingModel(splittedShelf[0], splittedShelf[1]);
            StorageLocationModel storageLocation = new StorageLocationModel(splittedShelf[2], splittedShelf[3], splittedShelf[4], building);
            data.Device.Location = storageLocation;
            //data.Device.Notes = "Placering redigeret";

            //send data to database
            data = dbManager.EditDeviceLocation(data);
            #endregion



            //send data to database
            int success = dbManager.EditDevice(data);
            dbManager.CreateLog(data);
            //set message to be shown in view
            if (success > 0)
            {
                ViewBag.edit = "Enhed Gemt";
            }
            else
            {
                ViewBag.edit = "Enhed ikke Gemt";
            }

            //get the logs again
            List<DeviceModel> logs = dbManager.GetAllDeviceLogs(data.Device.DeviceID);
            data.Logs = logs;

            //get locations again
            EditDeviceModel locations = dbManager.GetStorageLocations();
            data.Locations = locations.Locations;

            //get dropdwowns again
            data.Categories = dbsharedManager.GetCategories();
            data.ModelNames = dbsharedManager.GetModelNames();

            //download image
            int modelID = dbsharedManager.GetModelID(data.Device.Model.ModelName);
            ImageModel im = dbsharedManager.DownloadImage(modelID);

            //set image path if
            if (im.ImageData != null)
            {
                string newbase64 = Convert.ToBase64String(im.ImageData);
                string source = string.Format("data:image/png;base64,{0}", newbase64);
                data.ImagePath = source;
            }

            return View("EditView", data);
        }

        public IActionResult CategoryModelAdminView()
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            CategoryModelAdmin_Model data = GetAdminLists();
           
            return View(data);
        }

        [HttpPost]
        public IActionResult EditCategory(string edit, CategoryModelAdmin_Model data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            string[] split = edit.Split('-');
            int categoryIndex = int.Parse(split[0]);
            string categoryName = data.CategoryNames[categoryIndex];
            int id = int.Parse(split[1]);

            int feedback = dbManager.EditCategory(id, categoryName);
            data = GetAdminLists();

            return View("CategoryModelAdminView", data);
        }

        [HttpPost]
        public IActionResult EditModelName(string edit, CategoryModelAdmin_Model data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            string[] split = edit.Split('-');
            int modelIndex = int.Parse(split[0]);
            string modelName = data.ModelNames[modelIndex];
            int id = int.Parse(split[1]);

            int feedback = dbManager.EditModel(id, modelName);

            // CategoryModelAdmin_Model data = new CategoryModelAdmin_Model();
            data.ModelNames = dbsharedManager.GetModelNames();
            data.CategoryNames = dbsharedManager.GetCategories();
            data.CategoryIds = dbManager.GetAllCategoryIds();
            data.ModelIds = dbManager.GetAllModelIds();

            return View("CategoryModelAdminView", data);
        }

        [HttpPost]
        //Deactivate Device
        public IActionResult DeleteDevice(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            data.Device.ChangedBy = HttpContext.Session.GetString("uniLogin");

            data.Device.Status = 0;

            //change status of device to deactivated
            int success = dbManager.DeleteDevice(data);
            if (success > 0)
            {
                dbManager.CreateLog(data);
                ViewBag.Delete = "Enhed slettet";
                //create blank model
                data = new EditDeviceModel();
                DeviceModel device = new DeviceModel();
                data.Device = device;

                // clear model
                ModelState.Clear();


                ModelInfoModel info = new ModelInfoModel();
                info.SearchName = "L";
                info = dbManager.GetDeviceInventory(info);
                return View("Inventory", info);
            }
            else
            {
                ViewBag.ErrorDelete = "Enheden kan ikke slettes, da den er i brug";
                return View("EditView", data);
            }


            //  return View("EditView", data);

        }

        [HttpPost]
        //delete category
        public IActionResult DeleteCategory(string delete)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);


            int id = int.Parse(delete);
            int feedback = dbManager.DeleteCategory(id);
            CategoryModelAdmin_Model data = GetAdminLists();

            return View("CategoryModelAdminView", data);
        }

        [HttpPost]
        //delete modelName
        public IActionResult DeletModelName(string delete)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);


            int id = int.Parse(delete);
            int feedback = dbManager.DeleteModelName(id);
            CategoryModelAdmin_Model data = GetAdminLists();

            return View("CategoryModelAdminView", data);
        }

        public IActionResult Inventory(ModelInfoModel infoList)
        {
            //generate an instance of the database manager
            DBManagerDevice DBDManager = new DBManagerDevice(configuration);
            DBManagerShared Dbshared = new DBManagerShared(configuration);

            if(infoList.SearchName == null)
            {
                infoList.SearchName = "";
            }

            //set dummy data to database
            infoList.SearchName = infoList.SearchName;
            infoList.DeviceStatus = infoList.DeviceStatus;

            //set instock
            if (infoList.DeviceStatus == "Alle")
            {
                infoList.InStock = 2;
            }
            else if(infoList.DeviceStatus == "På Lager")
            {
                infoList.InStock = 1;
            }
            else if (infoList.DeviceStatus == "Udlånte")
            {
                infoList.InStock = 0;
            }

            infoList.Category = infoList.Category;
            infoList.Categories = Dbshared.GetCategories();
            infoList.InventoryStatuses = new List<string>() { "Alle", "På Lager", "Udlånte" };


            //get data from the manager
            infoList = DBDManager.GetDeviceInventory(infoList);




            return View(infoList);
        }


        public IActionResult AcquireDeviceDataForScanLocation(string deviceID)
        {
            EditDeviceModel data = new EditDeviceModel();
            data.Device = new DeviceModel();
            data.Device.DeviceID = Convert.ToInt32(deviceID);

            return ScanLocation(data);
        }

        //opens view for scanning of QR codes
        public IActionResult ScanLocation(EditDeviceModel data)
        {
            // When BookedDevices are returned, BookingController's "GoToLocationScanner()" will call this function, but "data" variable will be null. 
            if (TempData["deviceID"] != null)
            {
                data.Device = new DeviceModel();
                data.Device.DeviceID = Convert.ToInt32(TempData["deviceID"]);
            }
            data.Location = "";
            return View(data);
        }

        //return new data to Editview
        [HttpPost]
        public IActionResult ReturnScanData(EditDeviceModel data)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            string[] splittedData = data.Location.Split('-');
            EditDeviceModel newdata = new EditDeviceModel();
            DeviceModel device = new DeviceModel();
            bool LocationValid = true;

            //validate returned Scan data
            if (splittedData[0].Contains("Loc"))
            {
                //get data from splitted strings
                int id = data.Device.DeviceID;
                string[] location_string = splittedData[1].Split('.');

                //continue if array has the correct size
                if (location_string.Length == 5)
                {
                    //validate scanned data using DB manager
                    StorageLocationModel _location = new StorageLocationModel
                    {
                        Location = new BuildingModel { Building = location_string[0], RoomNumber = location_string[1] },
                        ShelfName = location_string[2],
                        ShelfLevel = location_string[3],
                        ShelfSpot = location_string[4]
                    };

                    if (dbManager.CheckLocation(_location))
                    {
                        //fill model with data
                        device = dbManager.GetDeviceInfoWithLocation(id);
                        newdata.Device = device;
                        newdata.Location = $"{location_string[0]}.{location_string[1]}.{location_string[2]}.{location_string[3]}.{location_string[4]}";
                        newdata = GetNewLocation(newdata);
                        newdata.Categories = dbsharedManager.GetCategories();
                        newdata.ModelNames = dbsharedManager.GetModelNames();
                        newdata.SelectedLogs = 10;
                    }
                    else
                    {
                        ViewBag.LocationError = "Lokation ikke fundet";
                        LocationValid = false;
                    }
                }
                else
                {
                    ViewBag.LocationError = "Den skannede QR kode er ikke en lokation";
                    LocationValid = false;
                }

            }
            else
            {
                LocationValid = false;
                ViewBag.LocationError = "Den skannede QR kode er ikke en lokation";
            }

            //return old model if location is not valid
            if (LocationValid == false)
            {
                device = dbManager.GetDeviceInfoWithLocation(data.Device.DeviceID);

                newdata.Device = device;
                newdata = GetNewLocation(newdata);
                newdata.Location = new string($"{device.Location.Location.Building}.{device.Location.Location.RoomNumber.ToString()}.{device.Location.ShelfName}.{device.Location.ShelfLevel}.{device.Location.ShelfSpot}");
                newdata.Categories = dbsharedManager.GetCategories();
                newdata.ModelNames = dbsharedManager.GetModelNames();
                newdata.SelectedLogs = 10;
            }


            if(TempData["bookingID"] != null)
            {
                EditDevice(newdata);
                return RedirectToAction("ReturnedFromLocationScanner", "Booking");
            }
            else
            {
                return View("EditView", newdata);
            }
        }

        //gather data to be sent to QR Generator
        [HttpPost]
        public IActionResult PrepForQR(EditDeviceModel input)
        {
            //add single string to list
            List<string> output = new List<string>();
            string data = $"Dev-{input.Device.DeviceID}-{input.Device.SerialNumber}";
            output.Add(data);


            //redirect to method
            return SendToQRController(output);
        }

        //send data to QR controller
        [HttpPost]
        public IActionResult SendToQRController(List<string> data)
        {
            TempData["QRData"] = data.ToArray();

            return RedirectToAction("PrintQR", "QRCode");
        }


        #region Helper methods
        //returns list of device logs
        private List<DeviceModel> GetDeviceLogs(int id)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            List<DeviceModel> data = dbManager.GetAllDeviceLogs(id);
            return data;
        }

        //returns all logs from device
        private List<DeviceModel> GetAllDeviceLogs(int id)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            List<DeviceModel> data = dbManager.GetAllDeviceLogs(id);
            return data;
        }

        //returns model with new location
        private EditDeviceModel GetNewLocation(EditDeviceModel data)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared shared = new DBManagerShared(configuration);

            //get the logs back again
            List<DeviceModel> logs = dbManager.GetAllDeviceLogs(data.Device.DeviceID);
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

            //get locations
            EditDeviceModel storagelocation = dbManager.GetStorageLocations();
            newdata.Locations = storagelocation.Locations;



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

        //unsafe code! might not get used
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

        //get categories & modelNames & their IDs
        private CategoryModelAdmin_Model GetAdminLists()
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            CategoryModelAdmin_Model viewmodelLists = new CategoryModelAdmin_Model();

            viewmodelLists.ModelNames = dbsharedManager.GetModelNames();
            viewmodelLists.CategoryNames = dbsharedManager.GetCategories();
            viewmodelLists.CategoryIds = dbManager.GetAllCategoryIds();
            viewmodelLists.ModelIds = dbManager.GetAllModelIds();
            return viewmodelLists;
        }

        #endregion


    }
}
