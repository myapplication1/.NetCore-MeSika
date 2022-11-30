using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeSika.Web.Pages.Shared
{
    public class _SideBar : PageModel
    {
        private readonly ILogger<HomeModel> _logger;
        private readonly List<Cards> accounts;
        public List<BankCards> Cards_ { get; set; }
        public _SideBar(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }
        public class BankCards
        {

            public string Id { get; set; }
            public string BankName { get; set; }
            public string Type { get; set; }
            public string AccountNumber { get; set; }
            public DateTime Expiry { get; set; }
            public string CardName { get; set; }
            public String img { get; set; }
            public decimal amount { get; set; }
            public string email { get; set; }


        }
        public async Task OnGet()
        {
           // await GetBankCards();
            //Options = Cards_.Select(a =>
            //                              new SelectListItem
            //                              {
            //                                  Value = a.AccountNumber.ToString(),
            //                                  Text = a.AccountNumber
            //                              }).ToList();
            //var res = await OnPostGetAPIBankCards();
            //var obj_ = JsonConvert.DeserializeObject<List<BankCards>>(res);
            //using (var client = new HttpClient())
            //{
            //    HttpRequestMessage request = new HttpRequestMessage();
            //    request.RequestUri = new Uri("https://app-api-pjs.herokuapp.com/api/BankCard");
            //    request.Method = HttpMethod.Get;
            //    //request.Headers.Add("SecureApiKey", "12345");
            //    HttpResponseMessage response = await client.SendAsync(request);
            //    var responseString = await response.Content.ReadAsStringAsync();
            //    var obj = JsonConvert.DeserializeObject<List<BankCards>>(responseString);
            //    var statusCode = response.StatusCode;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Cards = obj;                                    
            //    }


            //}

        }
        [BindProperty]
        public List<SelectListItem> Options { get; set; }
        //public async void OnGet()
        //{
        //    var res = await OnPostGetAPIBankCards();
        //    //Options = context.Authors.Select(a =>
        //    //                              new SelectListItem
        //    //                              {
        //    //                                  Value = a.AuthorId.ToString(),
        //    //                                  Text = a.Name
        //    //                              }).ToList();
        //}
        public async Task GetBankCards()
        {
            //BankCards Cards = new BankCards();
            var url = "https://app-api-pjs.herokuapp.com/api/BankCard/" + HttpContext.Session.GetString("UserLogged");
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            // var result = await response.Content.ReadAsStringAsync();
            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<BankCards>>(responseString);
            if (response.IsSuccessStatusCode)
            {
                Cards_ = obj;

                // return new JsonResult(result);
            }

            else
            {
                // return new JsonResult(response.IsSuccessStatusCode);
            }
            // return new JsonResult(result);


            //using (var client = new HttpClient())
            //{
            //    HttpRequestMessage request = new HttpRequestMessage();
            //    request.RequestUri = new Uri("https://app-api-pjs.herokuapp.com/api/BankCard" + HttpContext.Session.GetString("UserLogged"));
            //    request.Method = HttpMethod.Get;
            //    //request.Headers.Add("SecureApiKey", "12345");
            //    HttpResponseMessage response = await client.SendAsync(request);
            //    var responseString = await response.Content.ReadAsStringAsync();
            //    var obj = JsonConvert.DeserializeObject<List<BankCards>>(responseString);
            //    var statusCode = response.StatusCode;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Cards = obj;
            //    }


            //}


            //var url = "https://app-api-pjs.herokuapp.com/api/BankCard/" + HttpContext.Session.GetString("UserLogged");
            //using var client = new HttpClient();

            //var response = await client.GetAsync(url);

            //var result = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{

            //    return new JsonResult(result);
            //}

            //else
            //{
            //    return new JsonResult(response.IsSuccessStatusCode);
            //}
            // return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostGetAPIBankCards(
       )

        {



            var url = "https://app-api-pjs.herokuapp.com/api/BankCards/" + HttpContext.Session.GetString("UserLogged");
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
        public async Task<IActionResult> OnPostGetAPIWallet(
            string cname, string bname, string emails, string account,
             decimal amount, string type)
            
          {

            Random random = new Random();
            var rNumber = random.Next(0,5);
            Cards Cards = new Cards();
            Cards.BankName = bname;
            Cards.CardName = cname;
            //Cards.email = emails;
            Cards.Type = type;
            Cards.img = "img-"+rNumber;
            Cards.amount = amount;
            Cards.AccountNumber = account;
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

        public class Cards
        {
            public string Id { get; set; }
            public string email { get; set; }
            public decimal amount { get; set; }
            public string BankName { get; set; }
            public string Type { get; set; }
            public string AccountNumber { get; set; }
            public DateTime Expiry { get; set; }
            public string CardName { get; set; }
            public String img { get; set; }

        }
    }
}