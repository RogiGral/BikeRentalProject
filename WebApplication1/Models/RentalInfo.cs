using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RentalInfo
    {
        public int RentalInfoID { get; set; }
        public int UserID { get; set; }
        public int BikeID { get; set; }

        public virtual User User { get; set; }
        public virtual Bike Bike { get; set; }
    }
}