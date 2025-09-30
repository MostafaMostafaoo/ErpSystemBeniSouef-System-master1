using AutoMapper;
using ErpSystemBeniSouef.Core;
using ErpSystemBeniSouef.Core.Contract.Invoice.IDamageInvoiceService;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.CashInvoiceDto;
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
    public class DamageInvoiceItemService : IDamageInvoiceItemService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DamageInvoiceItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddInvoiceItems(AddCashInvoiceItemsDto dto)
        {
            var invoice = await _unitOfWork.Repository<Invoice>()
                .FindWithIncludesAsync(i => i.Id == dto.Id && i.invoiceType == InvoiceType.Damage, i => i.Supplier);

            if (invoice == null)
                throw new Exception($"Damage Invoice with Id {dto.Id} not found.");

            decimal totalAmount = invoice.TotalAmount ?? 0;

            foreach (var itemDto in dto.invoiceItemDtos)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new Exception($"Product with Id {itemDto.ProductId} not found.");

                var invoiceItem = new InvoiceItem
                {
                    InvoiceId = invoice.Id,
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ProductType = product.Category?.Name ?? "N/A",
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Notes = itemDto.Note
                };

                invoice.Items ??= new List<InvoiceItem>();
                invoice.Items.Add(invoiceItem);

                totalAmount += (itemDto.Quantity * itemDto.UnitPrice);
            }

            invoice.TotalAmount = totalAmount;

            _unitOfWork.Repository<Invoice>().Update(invoice);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<List<DamageInvoiceItemDetailsDto>> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            var items = await _unitOfWork.Repository<InvoiceItem>().GetAllAsync(i => i.InvoiceId == invoiceId);

            return items.Select(i => new DamageInvoiceItemDetailsDto
            {
                Id = i.Id,
                InvoiceId = i.InvoiceId,
                ProductName = i.ProductName,
                ProductType = i.ProductType,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Notes = i.Notes
            }).ToList();
        }

    }
}
