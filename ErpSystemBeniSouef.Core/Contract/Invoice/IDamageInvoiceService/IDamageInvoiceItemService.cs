using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.CashInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnDamageInvoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.Contract.Invoice.IDamageInvoiceService
{
    public interface IDamageInvoiceItemService
    {

        Task<bool> AddInvoiceItems(AddCashInvoiceItemsDto dto);

        Task<List<DamageInvoiceItemDetailsDto>> GetInvoiceItemsByInvoiceId(int invoiceId);
    }
}
