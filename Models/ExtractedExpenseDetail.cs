using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ExtractedExpenseDetail
    {

        public bool Success { get; set; }
        public string ValidationMessage { get; set; }
        public string CostCentre { get; set; }
        public string Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
