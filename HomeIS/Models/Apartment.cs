using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeIS.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        public Location Location { get; set; }
        public ApplicationUser Owner { get; set; }
        public string Description { get; set; }
        public int PropertyValue { get; set; }
        public int NumberOfRooms { get; set; }
        public int Size { get; set; }
        public bool Balcony { get; set; }
        public int FloorNumber { get; set; }

        // Entity Framework can't save an array
        public List<string> PhotoList { get; set; }
        public string Photos
        {
            get { return (PhotoList != null ? string.Join(",", PhotoList) : null); }
            set { PhotoList = value == null ? null : value.Split(',').ToList(); }
        }
    }
}