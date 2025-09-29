﻿using AutoMapper;
using ErpSystemBeniSouef.Core;
using ErpSystemBeniSouef.Core.Contract.Invoice.ReturnSupplir;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input.ReturnSupplirInvoiceDto;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Output.ReturnSupplirDto;
using ErpSystemBeniSouef.Core.Entities;
using ErpSystemBeniSouef.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Service.InvoiceServices.ReturnSupplierInvoiceService
{
    public class ReturnSupplierInvoiceItemService : IReturnSupplierInvoiceItemService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnSupplierInvoiceItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddInvoiceItems(AddReturnSupplierInvoiceItemsDto dto)
        {
            Invoice invoice = new Invoice();
            try
            {
                invoice = await _unitOfWork.Repository<Invoice>()
              .FindWithIncludesAsync(i => i.Id == dto.Id && i.invoiceType == InvoiceType.SupplierReturn, i => i.Supplier);

            }
            catch (Exception ex)
            {

            }

            if (invoice == null)
                return false;

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
                    UnitPrice = itemDto.UnitPrice
                };

                if (invoice.Items == null)
                    invoice.Items = new List<InvoiceItem>();
                invoice.Items.Add(invoiceItem);

                totalAmount += (itemDto.Quantity * itemDto.UnitPrice);
            }

            invoice.TotalAmount = totalAmount;

            _unitOfWork.Repository<Invoice>().Update(invoice);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<List<ReturnSupplierInvoiceItemDetailsDto>> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            var items = await _unitOfWork.Repository<InvoiceItem>()
                .GetAllAsync(i => i.InvoiceId == invoiceId);

            return items.Select(i => new ReturnSupplierInvoiceItemDetailsDto
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

        #region Soft Delete Invoice Region

        public bool SoftDelete(int id, decimal _totalLine, int _invoiceId)
        {
            var invoice = _unitOfWork.Repository<Invoice>().GetById(_invoiceId);
            if (invoice == null)
                return false;
            invoice.TotalAmount -= _totalLine;
            _unitOfWork.Repository<Invoice>().Update(invoice);


            var invoiceItem = _unitOfWork.Repository<InvoiceItem>().GetById(id);
            if (invoiceItem == null)
                return false;

            invoiceItem.IsDeleted = true;
            _unitOfWork.Repository<InvoiceItem>().Update(invoiceItem);

            _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
            if (invoice == null)
                return false;
            try
            {
                invoice.IsDeleted = true;
                _unitOfWork.Repository<Invoice>().Update(invoice);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion
    }
}
