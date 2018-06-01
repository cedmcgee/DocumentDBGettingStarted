using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBGettingStarted
{
    public class ContactInfo
    {
        public List<Phone> Phones { get; set;}
        public List<Email> Emails { get;set;}
        public List<Address> Addresses { get; set; }
    }
}
