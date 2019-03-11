using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Models;
using Microsoft.AspNetCore.Identity;

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
        public string Username { get; set; }
        public DateTime CreatonDate { get; set; } = DateTime.Now;
        public DateTime LastUsed { get; set; } = DateTime.Now;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
                            

            return Page();
        }

        [BindProperty]
        public ContactLink contactLink { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.contactLinks.Add(contactLink);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}