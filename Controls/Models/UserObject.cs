using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Controls.Models
{
    //
    // Summary:
    //     Overriden implementation of Microsoft.AspNetCore.Identity.UserObject`1
    //     which uses a string as a primary key.
    public class UserObject : IdentityUser
    {
        //
        // Summary:
        //     Initializes a new instance of Microsoft.AspNetCore.Identity.UserObject.
        //
        // Remarks:
        //     The Id property is initialized to form a new GUID string value.
        /*
        public UserObject();
        {
        string name = new Guid
        }
    //
    // Summary:
    //     Initializes a new instance of Microsoft.AspNetCore.Identity.IdentityUser.
    //
    // Parameters:
    //   userName:
    //     The user name.
    //
    // Remarks:
    //     The Id property is initialized to form a new GUID string value.
    public UserObject(string userName);
    {
    base(userName)
}
        */
    }
    public class UserRole : IdentityRole
    {
        public UserRole() : base() { }
        public UserRole(string roleName) { Name = roleName; }
    }
    /// <summary>
    /// Clients are the folk that come to us for service they will each be assigned a record locator
    /// locators as created as numbers that cannot be easily guessed by an attacker = semi-random
    /// uniqueness of the locator will be guaranteed by the database
    /// </summary>
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public UInt64 locator { get; set; }  // this is the record locator used to find the client data
        public string sub { get; set; }      // this is the subject ID that is used between the subject and authenticaotr
        public ulong created { get; set; }
        public ulong updated { get; set; }
        public string publicKey { get; set; }
        public string status { get; set; }
        public string purpose { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
    /// <summary>
    /// A request is issued for enabling access to a DOI it is only valid for a fixed time interval determined by the code
    /// A second time interval for validity is created on first use of the request; also determine by the code
    /// a thrid validity period is enabled by a count of the number of uses of the request; also determine by the code
    /// </summary>
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid cli_id { get; set; }  // not needed in the code base - could be the sub?
        public string doi { get; set; }   // coded in Google App format us.trustregistry.{base64 of locator}
        public ulong doi_date { get; set; }
        public ulong first_use { get; set; }
        public uint count_use { get; set; }
        public string status { get; set; }
        public string methods { get; set; }
        public string cert { get; set; }  // not needed in the code base
    }

}