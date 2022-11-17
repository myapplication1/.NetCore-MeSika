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
        //public ActionResult OnPostGetAPI()
        //{
        //    return RedirectToPage("Home");
        //}
        public async Task<IActionResult> OnPostGetAPI()
        {
            //await loginManager.SignOutAsync();
            var results = "pass";

            return new JsonResult(results);
        }

    }
}