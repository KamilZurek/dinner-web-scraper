using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerWebScraper
{
    public enum DinnerType
    {
        Soup,
        MainCourse
    }

    public static class DinnerTypeExtensions
    {
        public static string GetName(this DinnerType type)
        {
            return type switch
            {
                DinnerType.Soup => "Soup",
                DinnerType.MainCourse => "Main",
                _ => throw new Exception("Unhandled dinner type")
            };
        }
    }

    public class Dinner
    {
        public DateTime Date { get; set; }
        public DinnerType Type { get; set; }
        public string Name { get; set; }

        public Dinner(DateTime date, DinnerType dinnerType, string name)
        {
            this.Date = date;
            this.Type = dinnerType;
            this.Name = name;
        }
    }
}
