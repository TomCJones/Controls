using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Controls.Models
{

    public class ContactLink
    {
        /// <summary>
        /// conrol the means to contact a user for redress & recovery
        /// </summary>
        public ContactLink()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id{ get; set;}
        [ForeignKey("UserObject")]
        public string Parent{ get; set;}   // The subject (user)
        [ForeignKey("UserObject")]
        public string Child{ get; set;}    // The object (contact)
        [Required]
        public DateTime Creation{ get; set;}
        [Required]
        public DateTime LastUsed{ get; set;}
        public DateTime Expires{ get; set;}
        [Required]
        public string Type{ get; set;}   // modify relationship (medical, personal, employer etc.)
        [Required]
        public string Relationship{ get; set;}  //standards exist, but anything is acceptable
        public string GivenNames{ get; set;}
        public string FamilyName{ get; set;}
        public string Phones{ get; set;}
        public string AlternateEmail{ get; set;}
    }

    public class PhoneData  //  TODO make this json
    {
        public string PhoneType{ get; set;}
        public string PhoneDisplay{ get; set;}
        public ulong PhoneNumber{ get; set;}
    }
}
