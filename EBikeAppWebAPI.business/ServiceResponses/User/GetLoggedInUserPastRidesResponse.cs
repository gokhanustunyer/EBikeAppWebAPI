using EBikeAppWebAPI.business.ViewModel.Location;
using EBikeAppWebAPI.entity.Bike;
using EBikeAppWebAPI.entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ServiceResponses.User
{
    public class GetLoggedInUserPastRidesResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalPrice { get; set; }
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
    }
}
