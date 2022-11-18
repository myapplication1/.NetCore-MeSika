using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace MeSika.Web.Pages.Login

{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        public const string UserLogged = "UserLogged";
        public const string Fname = "Fname";
        public const string Lname = "Lname";
        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;

        }
        public async Task<IActionResult> OnGet()
        {
            //if (User.Identity.IsAuthenticated)
            //    return Redirect("/");
            //else 
                return Page();

        }
        public string startLoader { get; set; } = "inline-block";
        //public ActionResult OnPostGetAPI()
        //{
        //    return RedirectToPage("Home");
        //}
        public async Task<IActionResult> OnPostGetAPISignUp(string fname, string lname,string emails,string passwords)
        {
            Users user = new Users();
            user.FirstName = fname;
            user.LastName = lname;
            user.EmailAddress = emails;
            user.Password = passwords;  

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://app-api-pjs.herokuapp.com/api/Users/";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString(UserLogged, user.EmailAddress);
                HttpContext.Session.SetString(Fname, user.FirstName);
                HttpContext.Session.SetString(Lname, user.LastName);
                var identity = new ClaimsIdentity(new[]
                {
                                new Claim(ClaimTypes.Name,user.EmailAddress ?? string.Empty),
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
            return new JsonResult(result);
        }
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

          
        }
       
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/SignIn/Login");
        }
    }
}