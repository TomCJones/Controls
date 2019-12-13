using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Data;
using Controls.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Controls.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;
        string Authority = "net.azurewebsites.controls";

        public CreateModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Request = new Request();
            var query = HttpContext.Request.Query;
            StringValues sv;
            bool bSV = query.TryGetValue("id", out sv);
            string id = sv.FirstOrDefault();
            Request.doi = Authority + "." + id;
            ulong unixNow = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
            Request.doi_date = unixNow;
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