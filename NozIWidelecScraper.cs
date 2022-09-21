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
        private DateTime Date;

        public NozIWidelecScraper()
        {
            this.Web = new HtmlWeb();
            this.Doc = Web.Load(BaseUrl);
        }

        public List<Dinner> GetAllDinnersForToday()
        {
            var dinners = new List<Dinner>();

            this.Date = GetDate();
            dinners.Add(GetSoup());
            dinners.AddRange(GetMainCourses());

            return dinners;
        }

        private DateTime GetDate()
        {
            string fullDate = Doc.QuerySelector("#custom_post_widget-36")
                                .InnerText
                                .Replace("&nbsp;", "")
                                .Replace("\n", "");

            string shortDate = fullDate.Substring(fullDate.IndexOf(' ') + 1).Replace("r", "");

            return DateTime.Parse(shortDate);
        }

        private Dinner GetSoup()
        {
            var soup = Doc.QuerySelector("#custom_post_widget-34")
                .InnerText
                .Split('\n')[0];

            return new Dinner(this.Date, DinnerType.Soup, this.GetSoupName(soup)); 
        }

        private string GetSoupName(string soup) => Regex.Replace(soup, @"[^\u0020-\u007E\u00A0-\u00FF\u0100-\u017F]", string.Empty);

        private List<Dinner> GetMainCourses()
        {
            var mainCourses = new List<Dinner>();

            var dinners = Doc.QuerySelector("#custom_post_widget-24")
                .QuerySelectorAll("p")
                .Where(p => char.IsDigit(char.Parse(p.InnerText.Substring(0, 1))))
                .Select(n => n.InnerText);

            foreach (var dinner in dinners)
            {
                mainCourses.Add(new Dinner(this.Date, DinnerType.MainCourse, dinner));
            }

            return mainCourses;
        }
    }
}
