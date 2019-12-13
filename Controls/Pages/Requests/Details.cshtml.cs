using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Controls.Models;
using ZXing.QrCode;
using System.IO;

namespace Controls.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly Controls.Data.ControlsDbContext _context;

        public DetailsModel(Controls.Data.ControlsDbContext context)
        {
            _context = context;
        }
 
        public Request Request { get; set; }
        public string DoiInstance;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.requests.FirstOrDefaultAsync(m => m.Id == id);

            if (Request == null)
            {
                return NotFound();
            }
            // TODO set nocache because the image is not likely to be ever used again
            DoiInstance = Request.doi + ";" + Request.doi_date;   // TODO encode doi & data

            return Page();
        }
    }
}
