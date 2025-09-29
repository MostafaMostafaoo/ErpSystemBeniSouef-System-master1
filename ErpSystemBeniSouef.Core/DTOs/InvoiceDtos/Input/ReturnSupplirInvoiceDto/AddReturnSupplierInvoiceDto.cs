using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.ReturnSupplirInvoiceDto
{
    public class AddReturnSupplierInvoiceDto
    {
        public DateTime? InvoiceDate { get; set; }
        public int SupplierId { get; set; }
    }
}
