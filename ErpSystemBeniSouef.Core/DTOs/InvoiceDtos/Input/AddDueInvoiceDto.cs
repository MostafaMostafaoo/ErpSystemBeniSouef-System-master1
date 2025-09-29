﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input
{
    public class AddDueInvoiceDto
    {
        public DateTime InvoiceDate { get; set; }
        public int SupplierId { get; set; }
        public decimal? DueAmount { get; set; }
    }
}
