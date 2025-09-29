﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnSupplirDto
{
    public class ReturnSupplierInvoiceItemDetailsDto
    {
        public int DisplayId { get; set; } = 0;
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public string? Notes { get; set; }
    }
}
