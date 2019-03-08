using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Controls.Models
{
    //
    // Summary:
    //     The default implementation of Microsoft.AspNetCore.Identity.UserObject`1 which
    //     uses a string as a primary key.
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

}