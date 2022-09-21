namespace DinnerWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new NozIWidelecScraper();
            var dinners = scraper.GetAllDinnersForToday();

            SimpleSQLDataWriter.InsertDinnersIntoDB(dinners);
            SimpleSQLDataReader.GetDinnersFromDB();
            //dodac tabelke logi +_ check czy pobrano
        }
    }
}