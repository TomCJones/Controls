using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Controls.Areas.Identity.Pages.Account
{
    public class SigninRequiredModel : PageModel
    {
        public void OnGet()
        {
            var req = HttpContext.Request;
            var foo = req.ReadFormAsync();
            string bar = foo.ToString();
            string zingo = bar;
        }
    }
}