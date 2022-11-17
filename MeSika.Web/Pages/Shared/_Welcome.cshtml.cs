using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
namespace MeSika.Web.Pages.Shared
{
    public class _Welcome : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public _Welcome(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}