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

namespace Controls.Pages.Requests
{
    public class EditModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public EditModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.requests.FirstOrDefaultAsync(m => m.id == id);

            if (Request == null)
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

            _context.Attach(Request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.id))
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

        private bool RequestExists(ulong id)
        {
            return _context.requests.Any(e => e.id == id);
        }
    }
}
