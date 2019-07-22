using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class SaleTransaction
    {
        public Sale SaleModel { get; set; }
        public Transaction TransactionModel { get; set; }
    }
}