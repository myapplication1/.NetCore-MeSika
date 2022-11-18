using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace MeSika.Web.Pages.Shared
{
    public class _SideBar : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public _SideBar(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

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