using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileUpload.Models
{
    public class File
    {
        public int Sr_no { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> Age { get; set; }
    }
}