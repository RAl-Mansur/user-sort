﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserSort.Classes
{
    /// <summary>
    /// User class. Object will hold data imported from files 
    /// </summary>
    [XmlRoot(ElementName = "user")] 
    public class User
    {
        [XmlElement("userid")][DisplayName("User ID")] 
        public int user_id { get; set; }
        [XmlElement("firstname")][DisplayName("First Name")] 
        public string first_name { get; set; }
        [XmlElement("surname")][DisplayName("Last Name")] 
        public string last_name { get; set; }
        [XmlElement("username")][DisplayName("Username")] 
        public string username { get; set; }
        [XmlElement("type")][DisplayName("User Type")] 
        public string user_type { get; set; }
        [XmlElement("lastlogintime")][DisplayName("Last Login Time")]
        public string last_login_time { get; set; }

        public User() { }

        public User(int userID, string fName, string lName, string username, string type, string lastLogin)
        {
            this.user_id = userID;
            this.first_name = fName;
            this.last_name = lName;
            this.username = username;
            this.user_type = type;
            this.last_login_time = lastLogin;
        }


    }
}
