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

namespace Controls.Areas.Contacts
{
    public class EditContactModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public EditContactModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContactLink ContactLink { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactLink = await _context.contactLinks.FirstOrDefaultAsync(m => m.Id == id);

            if (ContactLink == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ContactLink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactLinkExists(ContactLink.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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
