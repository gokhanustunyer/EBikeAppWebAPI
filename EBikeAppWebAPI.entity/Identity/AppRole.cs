using EBikeAppWebAPI.entity.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.entity.Identity
{
    public class AppRole : IdentityRole
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
