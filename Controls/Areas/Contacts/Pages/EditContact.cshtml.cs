using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controls.Data;
using Controls.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Controls.Areas.Contacts
{
    public class EditContactModel : PageModel
    {
        private readonly UserManager<UserObject> _userManager;
        private readonly Controls.Data.ControlsDbContext _context;

        public EditContactModel(
            UserManager<UserObject> userManager,
            Controls.Data.ControlsDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // this is distinct from the cl to assure that a bad app does not try to manipulate the data base
        public class ContactEdit
        {
            [Key]
            public string Id { get; set; }
            public string ParentName { get; set; }   // The subject (user) typically the user name, which is often the email addr.
            [EmailAddress]
            public string ChildEmail { get; set; }    // The object (contact) displayed at the email address of the UserObject for the contact
            public DateTime Creation { get; set; }
            public DateTime LastUsed { get; set; }
            [DataType(DataType.DateTime)]
            public DateTime Expires { get; set; }
            [Required]
            public string RelType { get; set; }   // modify relationship (medical, personal, employer etc.)
            [Required]
            public string Relationship { get; set; }  //standards exist, but anything is acceptable
            [Required]
            public string GivenNames { get; set; }
            [Required]
            public string FamilyName { get; set; }
            public string Phones { get; set; }
            [EmailAddress]
            public string AlternateEmail { get; set; }
        }

        [BindProperty]
        public ContactEdit ce { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            ContactLink cl = await _context.contactLinks.FirstOrDefaultAsync(m => m.Id == id);

            if (cl == null)
            {
                return NotFound();
            }
            if (ce == null) { ce = new ContactEdit(); }
            ce.Id = cl.Id;
            ce.ParentName = await _userManager.GetUserNameAsync(user);
            ce.Creation = cl.Creation;
            ce.FamilyName = cl.FamilyName;
            ce.GivenNames = cl.GivenNames;
            ce.LastUsed = cl.LastUsed;
            ce.Expires = cl.Expires;
            ce.Phones = cl.Phones;
            ce.AlternateEmail = cl.AlternateEmail;
            ce.Relationship = cl.Relationship;
            ce.RelType = cl.Type;
            ce.ChildEmail = "email not found";
            UserObject child = await _userManager.FindByIdAsync(cl.Child);
            if (child != null) { ce.ChildEmail = child.Email; }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ContactLink cl = await _context.contactLinks.FirstOrDefaultAsync(m => m.Id == ce.Id);
            if (cl == null)
            {
                throw new Exception ("could not load contack link");
            }
            // verify that the contact is that of the signed in user!  (Security Requirement to avoid attack against the db)
            bool bChanged = false;   // The following are the only fields where a change will cause an update.
            if (ce.Phones != cl.Phones) { cl.Phones = ce.Phones; bChanged = true; }
            if (ce.AlternateEmail != cl.AlternateEmail) { cl.AlternateEmail = ce.AlternateEmail; bChanged = true; }
            if (ce.Relationship != cl.Relationship) { cl.Relationship = ce.Relationship; bChanged = true; }
            if (ce.RelType != cl.Type) { cl.Type = ce.RelType; bChanged = true; }
            if (ce.FamilyName != cl.FamilyName) { cl.FamilyName = ce.FamilyName; bChanged = true; }
            if (ce.GivenNames != cl.GivenNames) { cl.GivenNames = ce.GivenNames; bChanged = true; }
            if (ce.Expires != cl.Expires) { cl.Expires = ce.Expires; bChanged = true; }
            if (bChanged)
            {
                cl.LastUsed = DateTime.UtcNow;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactLinkExists(ce.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToPage("./Index");
        }

        private bool ContactLinkExists(string id)
        {
            return _context.contactLinks.Any(e => e.Id == id);
        }
    }
}
