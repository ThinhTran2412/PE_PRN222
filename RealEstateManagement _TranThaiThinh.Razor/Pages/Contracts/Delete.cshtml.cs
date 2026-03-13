using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Razor.Pages.Contracts
{
    public class DeleteModel : PageModel
    {
        private readonly RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext _context;

        public DeleteModel(RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract == null)
            {
                return NotFound();
            }
            else
            {
                Contract = contract;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                Contract = contract;
                _context.Contracts.Remove(Contract);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
