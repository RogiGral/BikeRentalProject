using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Parking
    {
        public int ParkingID { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public virtual ICollection<Bike> Bikes { get; set; }
    }
}