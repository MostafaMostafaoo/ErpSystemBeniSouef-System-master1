﻿using AutoMapper;
using ErpSystemBeniSouef.Core;
using ErpSystemBeniSouef.Core.Contract.Invoice.ReturnSupplir;
using ErpSystemBeniSouef.Core.DTOs.InvoiceDtos.Input;
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
    public class ReturnSupplierInvoiceService : IReturnSupplierInvoiceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnSupplierInvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public DtoForReturnSupplierInvoice AddInvoice(AddReturnSupplierInvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);

            // Make sure supplier exists
            var supplier = _unitOfWork.Repository<Supplier>().GetById(dto.SupplierId);
            if (supplier == null)
                return null;

            invoice.SupplierId = dto.SupplierId;
            invoice.Supplier = supplier;
            invoice.invoiceType = InvoiceType.SupplierReturn;
            invoice.TotalAmount = 0;
            invoice.CreatedDate = DateTime.UtcNow;

            _unitOfWork.Repository<Invoice>().Add(invoice);
            _unitOfWork.Complete();

            // Map Entity → Return DTO
            var returnDto = new DtoForReturnSupplierInvoice
            {
                Id = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = (decimal)invoice.TotalAmount,
                SupplierName = supplier.Name
            };

            return returnDto;
        }

        public async Task<IReadOnlyList<DtoForReturnSupplierInvoice>> GetAllAsync()
        {
            var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync(i => i.Supplier);
            var CahInvoice = invoices.Where(I => I.invoiceType == InvoiceType.SupplierReturn).ToList();

            var response = _mapper.Map<IReadOnlyList<DtoForReturnSupplierInvoice>>(CahInvoice);

            return response;
        }

        public async Task<ReturnSupplierInvoiceDetailsDto> GetInvoiceById(int id)
        {
            var invoice = await _unitOfWork.Repository<Invoice>()
               .FindWithIncludesAsync(i => i.Id == id && i.invoiceType == InvoiceType.SupplierReturn,
                                      i => i.Supplier,
                                      i => i.Items);

            if (invoice == null)
                return null;

            var response = _mapper.Map<ReturnSupplierInvoiceDetailsDto>(invoice);

            return response;
        }

        #region Update Invoice Dto Region

        public bool Update(UpdateInvoiceDto updateDto)
        {
            var invoice = _unitOfWork.Repository<Invoice>().GetById(updateDto.Id);
            if (invoice == null)
                return false;

            if (invoice.SupplierId != updateDto.SupplierId)
            {
                var supplier = _unitOfWork.Repository<Supplier>().GetById(updateDto.SupplierId);
                if (supplier == null)
                    return false;
            }

            _mapper.Map(updateDto, invoice);
            invoice.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Repository<Invoice>().Update(invoice);
            _unitOfWork.CompleteAsync();
            return true;
        }

        #endregion

        #region Soft Delete Invoice Region

        public bool SoftDelete(int id)
        {
            var product = _unitOfWork.Repository<Invoice>().GetById(id);
            if (product == null)
                return false;
            product.IsDeleted = true;
            _unitOfWork.Repository<Invoice>().Update(product);
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
