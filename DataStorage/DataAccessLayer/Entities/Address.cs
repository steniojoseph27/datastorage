using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Address
    {
        public int AddressID { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public int CountryID { get; set; }
        public int UserID { get; set; }
    }
}
