using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeIS.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public ApplicationUser Owner { get; set; }
        public string Description { get; set; }
        public int PropertyValue { get; set; }
    }
}