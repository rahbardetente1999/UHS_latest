using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class Request
    {
        public string Uid { get; set; }

        public string KeyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Amount { get; set; }

        override
        public string ToString()
        {
            return "Uid=" + this.Uid + " \nKeyId=" + this.KeyId + " \nFirstName=" + this.FirstName + " \nLastName=" + this.LastName + " \nEmail=" + this.Email + " \nPhone=" + this.Phone + " \nAmount=" + this.Amount;
        }
    }

}