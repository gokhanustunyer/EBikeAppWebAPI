using EBikeAppWebAPI.entity.Base;
using EBikeAppWebAPI.entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.entity.Ride
{
    public class Ride: BaseEntity
    {
        public Bike.Bike Bike { get; set; }
        public AppUser User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
    }
}
