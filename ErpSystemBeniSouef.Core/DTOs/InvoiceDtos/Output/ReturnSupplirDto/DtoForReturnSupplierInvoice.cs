using ErpSystemBeniSouef.Core.DTOs.SupplierDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnSupplirDto
{
    public class DtoForReturnSupplierInvoice

    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public SupplierRDto Supplier { get; set; }
        public decimal TotalAmount { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }

        // قائمة البنود (Items)
        //public List<ReturnSupplierInvoiceItemDetailsDto> InvoiceItemDtos { get; set; }
        //    = new List<ReturnSupplierInvoiceItemDetailsDto>();


    }
}
