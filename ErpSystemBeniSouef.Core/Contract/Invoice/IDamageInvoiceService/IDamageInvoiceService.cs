using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.DamageInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnDamageInvoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.Contract.Invoice.IDamageInvoiceService
{
    public interface IDamageInvoiceService
    {
        Task<ReturnDamageInvoiceDto> AddInvoice(AddDamageInvoiceDto dto);

        Task<DamageInvoiceDetailsDto> GetInvoiceById(int id);
    }
}
