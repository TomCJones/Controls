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
    public class IndexModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public IndexModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public IList<Request> Request { get;set; }

        public async Task OnGetAsync()
        {
            Request = (IList<Request>) await _context.requests.ToListAsync();
        }
    }
}
