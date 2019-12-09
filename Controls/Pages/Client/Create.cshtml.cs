using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controls.Data;
using Controls.Models;

namespace Controls.Pages.ClientPages
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
        public Client Client { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ulong unixNow = (ulong) DateTimeOffset.Now.ToUnixTimeSeconds();
            Client.created = unixNow;
            Client.updated = unixNow;
            // the following code creates a randon number to use as the locator code
            Guid gTest = Guid.NewGuid();
            byte[] gBytes = gTest.ToByteArray();
            int index = 0;
            Client.locator = BitConverter.ToUInt64(gBytes, index);
            string encoded = Convert.ToBase64String(gBytes);
            //         [System.CLSCompliant(false)]
            //     public static UInt64 ToUInt64(byte[] value, int startIndex);
            byte[] testit = Convert.FromBase64String(encoded);
            ulong uTestit = BitConverter.ToUInt64(testit, index);

            try
            {
                _context.clients.Add(Client);
            }
            catch (Exception e)   // the assumption here is that the only exception is an existing key - TODO test to see if that is the case
            {
                Client.locator += 1;   //  TODO create a test for this that is meaningfull
                _context.clients.Add(Client);
            }

            int iRet = await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}