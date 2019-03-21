using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Controls.Areas.Contacts.Pages
{
    public class AddContactModel : PageModel
    {
        private readonly UserManager<UserObject> _userManager;
        private readonly SignInManager<UserObject> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly Controls.Data.ControlsDbContext _context;

        public AddContactModel(UserManager<UserObject> userManager,
                        SignInManager<UserObject> signInManager,
                        IEmailSender emailSender,
                        Controls.Data.ControlsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }
        // these are set in the get
        public string Username { get; set; }
        [BindProperty]
        public string UserID { get; set; }
        [BindProperty]
        public DateTime Expiration { get; set; } 
        // these are set by the user
        [Required]
        [EmailAddress]
        [BindProperty]
        public string ContactEmail { get; set; }
        [BindProperty]
        public string RelType { get; set; }
        [Required]
        [BindProperty]
        public string Relationship { get; set; }
        [BindProperty]
        public string GivenNames { get; set; }
        [BindProperty]
        public string FamilyNames { get; set; }
        [BindProperty]
        public string Phones { get; set; }
        [BindProperty]
        public string Emails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var userID = await _userManager.GetUserIdAsync(user);
            int time2expireInYears = 10;   //  TODO create a config file with this kind of data  --  should this be a float type?
            TimeSpan countYears = new TimeSpan(365, 5, 48, 46) * time2expireInYears; //365.2422 days per year
            Expiration = DateTime.Now + countYears;
            RelType = "Personal";

            Username = userName;  //  just for display on web page
            UserID = userID;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cEmail = ContactEmail;
            UserObject contact = await _userManager.FindByEmailAsync(cEmail);
            if (contact == null)
            {
                contact = new UserObject { UserName = cEmail, Email = cEmail };
                var result = await _userManager.CreateAsync(contact);
                if (!result.Succeeded)
                { throw new SystemException("could not create user"); }
            }
            else
            {
                // TODO  inform uses that contact email is already in the db and what significance that might have for them
            }

            ContactLink contactLink = new ContactLink();
            contactLink.Parent = UserID;   // there is an edge case not covered if the user is deleted between get and post - even if it were valid now, the same could occur later as well - TODO need to handle that anyway
            contactLink.Child = contact.Id; // contact.Id;
            contactLink.Creation = DateTime.Now;
            contactLink.LastUsed = DateTime.Now;
            contactLink.Expires = Expiration;
            contactLink.Type = RelType;
            contactLink.Relationship = Relationship;
            contactLink.GivenNames = GivenNames;
            contactLink.FamilyName = FamilyNames;
            contactLink.Phones = Phones;
            contactLink.AlternateEmail = Emails;

            try
            {
                _context.contactLinks.Add(contactLink);
                await _context.SaveChangesAsync(); 
            }
            catch
            {
                throw new SystemException("could not create contact");   // TODO duplicates or write errors should not happen - but we should deal with them in a more friendly way if they do
            } 

            return RedirectToPage("./Display");  //  TODO make it possible to redirect to the page that brought us here
        }
    }
}