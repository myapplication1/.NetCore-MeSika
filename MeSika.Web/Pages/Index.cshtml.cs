using MeSika.Web.Pages.Modals;
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
        public async Task<IActionResult> OnPostGetAPIWallet(
          string cname, string bname, string emails, string account,
           decimal amount, string type)

        {
            var tt = HttpContext.Session.GetString("UserLogged");
            Random random = new Random();
            var rNumber = random.Next(1, 5);
            BankCards Cards = new BankCards
            {
                BankName = bname,
                CardName = cname,
                email = HttpContext.Session.GetString("UserLogged"),
                Type = type,
                Expiry = DateTime.UtcNow,
                img = "img-" + rNumber+".jpg",
                amount = amount,
                AccountNumber = account
            };
            var json = JsonConvert.SerializeObject(Cards);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://app-api-pjs.herokuapp.com/api/BankCard";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return new JsonResult(response.IsSuccessStatusCode);
            }

            else
            {
                return new JsonResult(response.IsSuccessStatusCode);
            }
            return new JsonResult(result);
        }
        public class BankCards
        {

            public string Id { get; set; }
            public string BankName { get; set; }
            public string Type { get; set; }
            public string email { get; set; }
            public string AccountNumber { get; set; }
            public DateTime Expiry { get; set; }
            public string CardName { get; set; }
            public String img { get; set; }
            public decimal amount { get; set; }


        }

        public async Task<IActionResult> OnPostGetAPIExpense(
          string description, string to, string account, string type, decimal amount , DateTime datePost
         )

        {

            
            Expense exp = new Expense();
            exp.From = account;
            exp.To = to;
            exp.Description = description;
            exp.email = HttpContext.Session.GetString("UserLogged"); ;
            exp.type = type;
            //Cards.email = emails;
            exp.Amount = amount;
            exp.Status = "Posted";
            exp.DateEntered = datePost;
            var json = JsonConvert.SerializeObject(exp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://app-api-pjs.herokuapp.com/api/Expense";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return new JsonResult(response.IsSuccessStatusCode);
            }

            else
            {
                return new JsonResult(response.IsSuccessStatusCode);
            }
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostGetAPIBankCards(      
        )

        {


          
            var url = "https://app-api-pjs.herokuapp.com/api/BankCards/"+ HttpContext.Session.GetString("UserLogged"); 
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return new JsonResult(response.IsSuccessStatusCode);
            }

            else
            {
                return new JsonResult(response.IsSuccessStatusCode);
            }
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostGetAPIIncome(
          string from, string account, string type, DateTime datePost , string amount)

        {

            Income exp = new Income();
            exp.From = from;
            exp.To = account;
           // exp.Description = description;
            exp.type = type;
            exp.email = HttpContext.Session.GetString("UserLogged");
            exp.Amount =Convert.ToDecimal(   amount);
            exp.Status = "Posted";
            exp.DateEntered = datePost;
            var json = JsonConvert.SerializeObject(exp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://app-api-pjs.herokuapp.com/api/Income";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return new JsonResult(response.IsSuccessStatusCode);
            }

            else
            {
                return new JsonResult(response.IsSuccessStatusCode);
            }
            return new JsonResult(result);
        }

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
                                new Claim(ClaimTypes.Name,obj.FirstName ?? string.Empty),
                                 new Claim(ClaimTypes.Email,email ?? string.Empty),
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
            return RedirectToPage("/");
        }
    }
}