using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public SignUpModel(ILogger<HomeModel> logger)
        {
            _logger = logger;

        }
    }
}