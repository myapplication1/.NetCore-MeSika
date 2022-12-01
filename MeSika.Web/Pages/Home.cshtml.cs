using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeSika.Web.Pages.Modals;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [ViewData]
        public string Message { get; set; }
        public string? Username { get; set; }
        public List<BankCards> Cards { get; set; }
        public List<Expense> Expense { get; set; }
        public List<Income> Income { get; set; }
        public async Task OnGet()
        {
            Message = "Hello World";
            await GetBankCards();
            //await IncomeList();
            await ExpensesList();
            Options = Cards.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.AccountNumber.ToString(),
                                              Text = a.AccountNumber
                                          }).ToList();
            ViewData["Account"] = Options;

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
        public async Task ExpensesList()
        {

            var url = "https://app-api-pjs.herokuapp.com/api/Expense/" + HttpContext.Session.GetString("UserLogged");
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            // var result = await response.Content.ReadAsStringAsync();
            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<Expense>>(responseString);
            if (response.IsSuccessStatusCode)
            {
                Expense = obj;

                // return new JsonResult(result);
            }




        }
        public async Task IncomeList()
        {

            var url = "https://app-api-pjs.herokuapp.com/api/Income/" + HttpContext.Session.GetString("UserLogged");
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            // var result = await response.Content.ReadAsStringAsync();
            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<Income>>(responseString);
            if (response.IsSuccessStatusCode)
            {
                Income = obj;

                // return new JsonResult(result);
            }




        }
        public async Task GetBankCards()
        {

            var url = "https://app-api-pjs.herokuapp.com/api/BankCard/" + HttpContext.Session.GetString("UserLogged");
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            // var result = await response.Content.ReadAsStringAsync();
            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<BankCards>>(responseString);
            if (response.IsSuccessStatusCode)
            {
                Cards = obj;

                // return new JsonResult(result);
            }


        }

    }
}