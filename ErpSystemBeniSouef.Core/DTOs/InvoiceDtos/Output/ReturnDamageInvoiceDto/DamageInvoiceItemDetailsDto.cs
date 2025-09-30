﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnDamageInvoiceDto
{
    public class DamageInvoiceItemDetailsDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public string? Notes { get; set; }
    }
}
