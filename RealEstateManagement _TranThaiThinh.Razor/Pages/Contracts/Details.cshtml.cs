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
    public class DetailsModel : PageModel
    {
        private readonly RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext _context;

        public DetailsModel(RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext context)
        {
            _context = context;
        }

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
    }
}
