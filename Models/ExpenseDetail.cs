using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class ExpenseDetail
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public decimal Gst { get; set; }
        public decimal TotalExcludingGst { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }


    }
}
