using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Controls.Data;
using Controls.Models;

namespace Controls.Areas.Contacts.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public DetailsModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

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
    }
}
