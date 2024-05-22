using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.entity.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<CreditCard.CreditCard> CreditCards { get; set; }
        public ICollection<Ride.Ride> Rides { get; set; }
        public string? CloseBikeToken { get; set; }

    }
}
