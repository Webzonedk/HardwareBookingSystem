using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HUS_project.Models
{
    public class Device
    {
        #region Fields
        private int id;
        private string description;
        private string category;
        private string room;
        private List<string> notes;
        #endregion

        #region Properties
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        public string Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
            }
        }
        public List<string> Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }
        #endregion
    }
}
