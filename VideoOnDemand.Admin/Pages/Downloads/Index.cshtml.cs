using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace VideoOnDemand.Admin.Pages.Downloads
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IDbReadService _dbReadService;
        public IEnumerable<Download> Items = new List<Download>();
        [TempData] public string StatusMessage { get; set; }

        public IndexModel(IDbReadService dbReadService)
        {
            _dbReadService = dbReadService;
        }

        public void OnGet()
        {
            Items = _dbReadService.GetWithIncludes<Download>();
        }
    }
}