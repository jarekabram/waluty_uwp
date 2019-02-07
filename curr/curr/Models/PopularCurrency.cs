using System;
using System.Collections;
using System.Collections.Generic;

namespace curr.Models
{
    public class Rate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public double Mid { get; set; }
    }

    public class PopularCurrency
    {
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }
}