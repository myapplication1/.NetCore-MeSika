using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages.BankCards
{
    public class _WalletModel : PageModel
    {
        private readonly ILogger<_WalletModel> _logger;

        public _WalletModel(ILogger<_WalletModel> logger)
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