﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.Entities
{
    public class Country
    {
        public int CountryID { get; set; }
        public string? Name { get; set; }
    }
}
