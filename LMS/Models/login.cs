using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class login
    {
        [DisplayName("User ID")]
        public string u_id { get; set; }
        [DisplayName("User Password"), DataType(DataType.Password)]
        public string u_password { get; set; }
        [DisplayName("Position")]
        public string u_role { get; set; }
    }
}