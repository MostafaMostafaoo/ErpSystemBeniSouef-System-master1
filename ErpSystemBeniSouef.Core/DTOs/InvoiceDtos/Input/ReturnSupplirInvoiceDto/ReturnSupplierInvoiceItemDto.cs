﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.ReturnSupplirInvoiceDto
{
    public class ReturnSupplierInvoiceItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductTypeName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public string Notes { get; set; }
        public int InvoiceId { get; set; }

    }
}
