﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.CashInvoiceDto
{
    public class AddCashInvoiceItemsDto
    {
        public int Id { get; set; }
        public List<CashInvoiceItemDto> invoiceItemDtos { get; set; }
    }
}
