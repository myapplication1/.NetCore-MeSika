using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages
{
    public class _MoneyCards : PageModel
    {
        private readonly ILogger<_MoneyCards> _logger;

        public _MoneyCards(ILogger<_MoneyCards> logger)
        {
            _logger = logger;
        }
        public string? Name { get; set; }
        public void OnGet()
        {
            Name = "Krissss";
        }
    }
}