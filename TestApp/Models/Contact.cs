using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact_List.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string contactName { get; set; }
        public string contactSurname { get; set; }
        public string contactEmail { get; set; }
        public string telephoneNumber { get; set; }
        public string description { get; set; }

        public int UserId { get; set; }
        public User User;

        public void setInformation(Contact c)
        {
            contactName = c.contactName;
            contactSurname = c.contactSurname;
            contactEmail = c.contactEmail;
            telephoneNumber = c.telephoneNumber;
            description = c.description;                 
        }
    }
}
