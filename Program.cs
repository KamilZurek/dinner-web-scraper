using DinnerWebScraper.SQL;

namespace DinnerWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new NozIWidelecScraper();
            var date = scraper.GetDate();

            if (!SimpleSQLDataReader.IsDinnerDateAlreadyInDB(date))
            {
                var dinners = scraper.GetAllDinnersForToday();
                SimpleSQLDataWriter.InsertDinnersIntoDB(dinners);
                SimpleSQLDataWriter.InsertDinnerDateIntoDB(date);
            }
            
            SimpleSQLDataReader.GetDinnersFromDB();
            //check all once again
        }
    }
}