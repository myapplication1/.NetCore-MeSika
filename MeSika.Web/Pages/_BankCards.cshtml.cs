using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages.BankCards
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            OnGet();
        }
        public string? Name { get; set; }
        public void OnGet()
        {
            Name = "KKKKKKKKKKKKKKKK";
        }
    }
}