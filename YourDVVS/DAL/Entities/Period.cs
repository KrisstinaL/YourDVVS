using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class Period
    {
        public int PeriodId { get; set; }
        public DateTime PeriodBegining { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}