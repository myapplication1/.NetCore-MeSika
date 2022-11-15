using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages.Shared
{
    public class _SideBar : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public _SideBar(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}