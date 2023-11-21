using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Credential
    {
        public int CredentialID { get; set; }
        public int UserID { get; set; }
        public string? PasswordHash {get; set; }
    }
}
