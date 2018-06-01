using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBGettingStarted
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; } = true;
        public bool Square { get; set; } = true;
        public bool Demitted { get; set; } = false;
        public string Position { get; set; }
        public DateTime DOB { get; set; }
        public ContactInfo Contact { get;set;}
        public EmergencyContact EmergencyContact { get; set; }
      
    }
}
