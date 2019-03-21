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
        [BindProperty]
        public string Redir { get; set; }

        public void OnGet()
        {
            var reqQuery = HttpContext.Request.Query;
            Redir = null;
            if (reqQuery != null)
            {
                string redir = reqQuery.FirstOrDefault().Key;
                Redir = redir;
            }

        }
    }
}