using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateManagement__TranThaiThinh.Repositories.Models;
using RealEstateManagement__TranThaiThinh.Services;

namespace RealEstateManagement__TranThaiThinh.Razor.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly IContractService _context;

        public IndexModel(IContractService context)
        {
            _context = context;
        }

        public IList<Contract> Contracts { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var allcontract = await _context.GetAllAsync();

            allcontract = allcontract.OrderByDescending(p => p.SigningDate).ToList();
            Contracts = allcontract;
        }

    }
}

