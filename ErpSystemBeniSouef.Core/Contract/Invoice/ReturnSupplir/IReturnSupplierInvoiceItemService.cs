using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.ReturnSupplirInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnSupplirDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.Contract.Invoice.ReturnSupplir
{
    public interface IReturnSupplierInvoiceItemService
    {
        #region add Region
        Task<bool> AddInvoiceItems(AddReturnSupplierInvoiceItemsDto dto);

        #endregion
        #region Get Invoice Items By InvoiceId Region
        Task<List<ReturnSupplierInvoiceItemDetailsDto>> GetInvoiceItemsByInvoiceId(int invoiceId);

        #endregion
        #region Soft Delete Region

        bool SoftDelete(int id, decimal totalLine, int invoiceId);

        Task<bool> SoftDeleteAsync(int id);

        #endregion
    }
}
