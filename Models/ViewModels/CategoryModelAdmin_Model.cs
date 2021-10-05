using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class CategoryModelAdmin_Model
    {
        //fields
        private List<string> modelNames;
        private List<int> modelIds;

        private List<string> categoryNames;
        private List<int> categoryIds;

        private string new_category;
        private string editedCategory;
        private string editedModelName;
        private int editedID;


        public int EditedID
        {
            get { return editedID; }
            set { editedID = value; }
        }
        public string EditedModelName
        {
            get { return editedModelName; }
            set { editedModelName = value; }
        }
        public string EditedCategory
        {
            get { return editedCategory; }
            set { editedCategory = value; }
        }
        public string New_Category
        {
            get { return new_category; }
            set { new_category = value; }
        }




        //properties
        public List<string> ModelNames { get => modelNames; set => modelNames = value; }
        public List<int> ModelIds { get => modelIds; set => modelIds = value; }
        public List<string> CategoryNames { get => categoryNames; set => categoryNames = value; }
        public List<int> CategoryIds { get => categoryIds; set => categoryIds = value; }

        //constructor
        public CategoryModelAdmin_Model ()
        {

        }
    }
}
