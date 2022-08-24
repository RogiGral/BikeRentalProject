using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Bike
    {
        public int BikeID { get; set; }
        public int ParkingID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public float PricePerHour { get; set; }
        public virtual ICollection<RentalInfo> RentalInfos { get; set; }
        public virtual Parking Parking { get; set; }
    }
}