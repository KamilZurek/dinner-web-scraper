namespace DinnerWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new NozIWidelecScraper();
            scraper.GetAllDinnersForToday();
        }
    }
}