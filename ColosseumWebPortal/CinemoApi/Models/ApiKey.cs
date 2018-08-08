using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemoApi.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string KeyString { get; set; }
        public string UserId { get; set; }
    }
}