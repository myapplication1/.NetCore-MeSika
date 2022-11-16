using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeSika.Web.Pages
{
    public class HomeModel : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public HomeModel(ILogger<HomeModel> logger)
        {
            _logger = logger;
            OnGet();
        }
        public string? Username { get; set; }
        public List<Cards>? Cards { get; set; }
        public void OnGet()
        {
            Username = "kamoah";

            Cards = new List<Cards>();
            var Cards_ = new Cards();
            Cards_.CardName = "Kristotro Amoah";
            Cards_.AccountNumber = "3414-234X";
            Cards_.Type = "VISA-CREDIT";
            Cards_.img = "img-1.jpg";
            Cards.Add(Cards_);

            var Cards__ = new Cards();
            Cards__.CardName = "Kristotro Amoah";
            Cards__.AccountNumber = "0565-452X";
            Cards__.Type = "VISA-DEBIT";
            Cards__.img = "img-2.jpg";
            Cards.Add(Cards__);

            var Cards___ = new Cards();
            Cards___.CardName = "Kristotro Amoah";
            Cards___.AccountNumber = "1234-234X";
            Cards___.Type = "VISA-DEBIT";
            Cards___.img = "img-3.jpg";
            Cards.Add(Cards___);

            var Cards_1 = new Cards();
            Cards_1.CardName = "Kristotro Amoah";
            Cards_1.AccountNumber = "4124-854X";
            Cards_1.Type = "MASTER-DEBIT";
            Cards_1.img = "img-5.jpg";
            Cards.Add(Cards_1);

            var Cards_2 = new Cards();
            Cards_2.CardName = "Kristotro Amoah";
            Cards_2.AccountNumber = "1747-5334X";
            Cards_2.Type = "VISA-DEBIT";
            Cards_2.img = "img-1.jpg";
            Cards.Add(Cards_2);
        }
    }

    public class Cards
    {
        public string Id { get; set; }
        public string BankName { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
        public string CardName { get; set; }
        public String img { get; set; }

    }
}