using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calculation;

namespace Calculation
{
    public static class Calculator
    {
        public static GstDetail CalculateGstDetailFromGstInclusiveAmount(decimal amount)
        {
            //In production env would expect Gst rate to be stored in database to allow for modification without recompile of code. Alternative is a config file but then
            //requires changing per application

            decimal gstRate = Constants.GstRate;
            decimal gstAmount = amount / gstRate * 15;
            decimal amountExcGst  = (amount * 100) / gstRate;

            return new GstDetail
            {
                AmountIncludingGst = amount,
                AmountExcludingGst = amountExcGst,
                GstAmount = gstAmount
            };
        }
    }
}
