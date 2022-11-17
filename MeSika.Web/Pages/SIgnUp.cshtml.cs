using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Xml.Linq;

namespace MeSika.Web.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;

        public SignUpModel(ILogger<SignUpModel> logger)
        {
            _logger = logger;

        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Home");
            else return Redirect("/"); ;

        }
        public string startLoader { get; set; } = "inline-block";
        //public ActionResult OnPostGetAPI()
        //{
        //    return RedirectToPage("Home");
        //}
        public async Task<IActionResult> OnPostGetAPI(Users user)
        {
            
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://app-api-pjs.herokuapp.com/api/Users/" + email + "/" + password);
                request.Method = HttpMethod.Post;
                //request.Headers.Add("SecureApiKey", "12345");
                HttpResponseMessage response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Users>(responseString);
                var statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString(UserLogged, email);
                    HttpContext.Session.SetString(Fname, obj.FirstName);
                    HttpContext.Session.SetString(Lname, obj.LastName);
                    var identity = new ClaimsIdentity(new[]
                    {
                                new Claim(ClaimTypes.Name,email ?? string.Empty),
                                //new Claim(ClaimTypes.Role,totalResults.Tables[0].Rows[0]["Program_Name"].ToString() ?? string.Empty)
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return new JsonResult(response.IsSuccessStatusCode);
                }

                else
                {
                    return new JsonResult(response.IsSuccessStatusCode);
                }
            }
            return new JsonResult(null);
        }
        public class Users
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string DOB { get; set; }
            public string EmailAddress { get; set; }
            public string Phone { get; set; }
            public string PhotoUrl { get; set; }
            public string Password { get; set; }

            public object events { get; set; }
            public List<object> guests { get; set; }
        }
        public class UsersDTO
        {

            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string DOB { get; set; }
            public string EmailAddress { get; set; }
            public string Phone { get; set; }
            public string PhotoUrl { get; set; }
            public string Password { get; set; }

            public object events { get; set; }
            public object guests { get; set; }
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/SignIn/Login");
        }
    }
}

