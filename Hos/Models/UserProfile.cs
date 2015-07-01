using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hos.Models
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int National_ID_Number { get; set; }
        public string RoleName { get; set; }

        public string SurName { get; set; }
    }


}