using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeSika.Web.Pages.Modals;
using Newtonsoft.Json;

namespace MeSika.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public HomeModel(ILogger<HomeModel> logger)
        {
            _logger = logger;
           // OnGet();
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
        public string? Username { get; set; }
        public List<BankCards> Cards { get; set; }
        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://app-api-pjs.herokuapp.com/api/BankCard");
                request.Method = HttpMethod.Get;
                //request.Headers.Add("SecureApiKey", "12345");
                HttpResponseMessage response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<List<BankCards>>(responseString);
                var statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Cards = obj;                                    
                }

               
            }
          
        }
    }

  
}