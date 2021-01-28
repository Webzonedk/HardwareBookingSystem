using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HUS_project.Models
{
    public class UserLoginDataModel
    {
        [Required]
        private string uniLogin;
        [Required]
        private string password;

        public string UNILogin
        {
            get { return uniLogin; }
            set { uniLogin = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public UserLoginDataModel() { }
        public UserLoginDataModel(string UNILogin, string Password)
        {
            uniLogin = UNILogin;
            password = Password;
        }

    }
}
