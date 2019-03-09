using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Data;
using Controls.Models;

namespace Controls.Areas.Contacts
{
    public class AddContactModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public AddContactModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContactLink ContactLink { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.contactLinks.Add(ContactLink);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}