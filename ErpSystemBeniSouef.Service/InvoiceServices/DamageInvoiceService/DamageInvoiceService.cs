using AutoMapper;
using ErpSystemBeniSouef.Core;
using ErpSystemBeniSouef.Core.Contract.Invoice;
using ErpSystemBeniSouef.Core.Contract.Invoice.IDamageInvoiceService;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.CashInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.DamageInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnDamageInvoiceDto;
using ErpSystemBeniSouef.Core.Entities;
using ErpSystemBeniSouef.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Service.InvoiceServices.DamageInvoiceService
{
    public class DamageInvoiceService : IDamageInvoiceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DamageInvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReturnDamageInvoiceDto> AddInvoice(AddDamageInvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);

            //var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(dto.SupplierId);
            //if (supplier == null)
            //    throw new Exception($"Supplier with Id {dto.SupplierId} not found.");

            //invoice.SupplierId = dto.SupplierId;
            // invoice.Supplier = supplier;
            invoice.invoiceType = InvoiceType.Damage;
            invoice.TotalAmount = 0;
            invoice.CreatedDate = DateTime.UtcNow;

            _unitOfWork.Repository<Invoice>().Add(invoice);
            await _unitOfWork.CompleteAsync();

            return new ReturnDamageInvoiceDto
            {
                Id = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount ?? 0,
                SupplierName = "" //supplier.Name
            };
        }
        public async Task<DamageInvoiceDetailsDto> GetInvoiceById(int id)
        {
            var invoice = await _unitOfWork.Repository<Invoice>()
                .FindWithIncludesAsync(i => i.Id == id && i.invoiceType == InvoiceType.Damage && !i.IsDeleted, i => i.Supplier);

            if (invoice == null)
                throw new Exception($"Damage Invoice with Id {id} not found.");

            return new DamageInvoiceDetailsDto
            {
                Id = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount ?? 0,
                SupplierName = invoice.Supplier?.Name ?? "N/A",
                Notes = invoice.Notes
            };
        }
    }
}
