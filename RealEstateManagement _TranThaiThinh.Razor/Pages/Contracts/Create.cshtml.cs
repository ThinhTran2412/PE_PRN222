using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Razor.Pages.Contracts
{
    public class CreateModel : PageModel
    {
        private readonly RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext _context;

        public CreateModel(RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BrokerId"] = new SelectList(_context.Brokers, "BrokerId", "Address");
            return Page();
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contracts.Add(Contract);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
