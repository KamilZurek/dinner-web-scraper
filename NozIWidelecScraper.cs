using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DinnerWebScraper
{
    class NozIWidelecScraper
    {
        private const string BaseUrl = "https://nozwideleccatering.pl/";

        private HtmlWeb Web;
        private HtmlDocument Doc;

        public NozIWidelecScraper()
        {
            this.Web = new HtmlWeb();
            this.Doc = Web.Load(BaseUrl);
        }

        public void GetAllDinnersForToday()
        {
            var date = GetDate();
            var soup = GetSoup();
            var dinners = GetDinners();

            Console.WriteLine($"Dzień: {date}\nZupa: {soup}\nDrugie danie:\n{dinners}");
        }

        private string GetDate() => Doc.QuerySelector("#custom_post_widget-36")
            .InnerText
            .Replace("&nbsp;", "")
            .Replace("\n", "");

        private string GetSoup()
        {
            var soup = Doc.QuerySelector("#custom_post_widget-34")
                .InnerText
                .Split('\n')[0];

            return Regex.Replace(soup, @"[^\u0000-\u007F]+", string.Empty);
        }

        private string GetDinners()
        {
            var dinners = Doc.QuerySelector("#custom_post_widget-24")
                .QuerySelectorAll("p")
                .Where(p => char.IsDigit(char.Parse(p.InnerText.Substring(0, 1))))
                .Select(n => n.InnerText);

            return string.Join('\n', dinners);
        }
    }
}
