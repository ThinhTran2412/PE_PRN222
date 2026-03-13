using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Razor.Pages.Contracts
{
    public class EditModel : PageModel
    {
        private readonly RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext _context;

        public EditModel(RealEstateManagement__TranThaiThinh.Repositories.Models.FA25RealEstateDBContext context)
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

            var contract =  await _context.Contracts.FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }
            Contract = contract;
           ViewData["BrokerId"] = new SelectList(_context.Brokers, "BrokerId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(Contract.ContractId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }
    }
}
