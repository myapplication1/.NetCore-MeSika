using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Xml.Linq;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MeSika.Web.Pages
{
    [AllowAnonymous]
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

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://app-api-pjs.herokuapp.com/api/Users/";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
       
            return new JsonResult(result);
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

