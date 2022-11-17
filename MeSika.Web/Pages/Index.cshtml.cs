using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
        public async Task<IActionResult> OnPostGetAPI(string email, string password)
        {
            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, email)
            //};
            //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    var principal = new ClaimsPrincipal(identity);
            //    await HttpContext.SignInAsync(principal);
            //    //await loginManager.SignOutAsync();
            //    var results = "pass";
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://app-api-pjs.herokuapp.com/api/Users/" + email + "/" + password);
                request.Method = HttpMethod.Get;
                //request.Headers.Add("SecureApiKey", "12345");
                HttpResponseMessage response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    //API call success, Do your action
                    return new JsonResult(response.IsSuccessStatusCode);
                }

                else
                {
                    return new JsonResult(response.IsSuccessStatusCode);
                }
            }
            return new JsonResult(null);
        }
    }
}