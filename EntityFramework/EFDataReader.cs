using DinnerWebScraper.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerWebScraper.EntityFramework
{
    public class EFDataReader
    {
        public static void GetDinnersFromDB()
        {
            using (var context = new DinnersManagerContext())
            {
                Console.WriteLine("Odczyt EF:\n{0}\t{1}\t{2}\t{3}\n", "ID", "Typ", "Nazwa dania", "Data");
                
                foreach (var dinner in context.Dinners
                                                .OrderBy(p => p.Id))
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", dinner.Id, dinner.Type, dinner.Name, dinner.Date);
                }
            }
        }
    }
}
