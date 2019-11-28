using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Controls.Data;
using Controls.Models;

namespace Controls.Pages.ClientPages
{
    public class DetailsModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public DetailsModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public Client Client { get; set; }

        public async Task<IActionResult> OnGetAsync(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = await _context.clients.FirstOrDefaultAsync(m => m.locator == id);

            if (Client == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
