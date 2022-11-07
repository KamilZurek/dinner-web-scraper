using System;
using System.Collections.Generic;

namespace DinnerWebScraper.EntityFramework.Models
{
    public partial class DinnersStatus
    {
        public DateTime DinnersDate { get; set; }
        public DateTime Downloaded { get; set; }
        public int Id { get; set; }
    }
}
