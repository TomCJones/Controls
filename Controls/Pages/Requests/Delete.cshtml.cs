using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Controls.Data;
using Controls.Models;

namespace Controls.Pages.Requests
{
    public class DeleteModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public DeleteModel(Controls.Data.ControlsDbContext context)
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

        public async Task<IActionResult> OnPostAsync(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.requests.FindAsync(id);

            if (Request != null)
            {
                _context.requests.Remove(Request);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
