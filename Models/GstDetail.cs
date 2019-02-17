using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GstDetail
    {
        public decimal AmountIncludingGst { get; set; }
        public decimal AmountExcludingGst { get; set; }
        public decimal GstAmount { get; set; }
    }
}
