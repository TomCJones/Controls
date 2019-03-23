using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Controls.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Controls.Areas.Identity.Pages.Account
{
    public class SigninRequiredModel : PageModel
    {
        private readonly SignInManager<UserObject> _signInManager;
        private readonly UserManager<UserObject> _userManager;
        private readonly ILogger<ExternalLoginModel> _logger;

        public SigninRequiredModel(
            SignInManager<UserObject> signInManager,
            UserManager<UserObject> userManager,
            ILogger<ExternalLoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public string Redir { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // the following code is provided to accommodate the Apple iOS 12 same-site problem
            var req = HttpContext.Request.Host;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {

                return Redirect("https://" + req.ToString() + "/Contacts/Display");
            }

            Redir = "Display";  //  this is where we want to go after a successful login
            return Page();
        }
    }
}