using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Data;
using Controls.Models;

namespace Controls.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public CreateModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.requests.Add(Request);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}