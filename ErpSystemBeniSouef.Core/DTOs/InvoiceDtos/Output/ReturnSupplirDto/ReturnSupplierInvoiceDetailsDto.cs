﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnSupplirDto
{
    public class ReturnSupplierInvoiceDetailsDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string SupplierName { get; set; }
        public string? Notes { get; set; }

        public List<ReturnSupplierInvoiceItemDetailsDto> Items { get; set; }
            = new List<ReturnSupplierInvoiceItemDetailsDto>();
    }

}
