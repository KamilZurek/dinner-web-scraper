using System;
using System.Collections.Generic;

namespace DinnerWebScraper.EntityFramework.Models
{
    public partial class Dinner
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
