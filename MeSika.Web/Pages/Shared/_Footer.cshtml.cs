using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages.Shared
{
    public class _Footer : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public _Footer(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}