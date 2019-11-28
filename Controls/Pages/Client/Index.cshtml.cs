// controls index.cs copyright tomjones

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
    public class IndexModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public IndexModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; }

        public async Task OnGetAsync()
        {
            Client = await _context.clients.ToListAsync();
        }
    }
}
