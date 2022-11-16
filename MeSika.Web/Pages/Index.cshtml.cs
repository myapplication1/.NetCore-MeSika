using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;

        }

        public string startLoader { get; set; } = "inline-block";
        public async Task<IActionResult> OnGetGetAPI()
        {           
            return Page();
        }
       
    }
}