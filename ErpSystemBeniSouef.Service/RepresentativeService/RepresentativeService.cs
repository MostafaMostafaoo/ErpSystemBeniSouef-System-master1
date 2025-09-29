﻿using AutoMapper;
using ErpSystemBeniSouef.Core;
using ErpSystemBeniSouef.Core.Contract;
using ErpSystemBeniSouef.Core.DTOs.RepresentativeDto;
using ErpSystemBeniSouef.Core.DTOs.SubAreaDtos;
using ErpSystemBeniSouef.Core.DTOs.SupplierDto;
using ErpSystemBeniSouef.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Service.RepresentativeService
{
    public class RepresentativeService : IRepresentativeService
    {
        #region Constractor Region

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RepresentativeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Create Region

        public RepresentativeDto Create(CreateRepresentativeDto createDto)
        {
            try
            {
                var representative = _mapper.Map<Representative>(createDto);
                _unitOfWork.Repository<Representative>().Add(representative);
                _unitOfWork.Complete();


                return _mapper.Map<RepresentativeDto>(representative);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Get All Async  Region

        public async Task<IReadOnlyList<RepresentativeDto>> GetAllAsync()
        {
            var representative = await _unitOfWork.Repository<Representative>().GetAllAsync();
            var representativeDto = representative.Select(sa => _mapper.Map<RepresentativeDto>(sa)).ToList();

            return representativeDto;
        }

        #endregion

        #region  Soft Delete Region

        public bool SoftDelete(int id)
        {
            Representative representative = _unitOfWork.Repository<Representative>().GetById(id);
            if (representative == null)
                return false;
            try { representative.IsDeleted = true; _unitOfWork.Complete(); return true; }
            catch { return false; }
        }

        #endregion

        #region Update Region

        public bool Update(UpdateRepresentativeDto updateDto)
        {
            Representative representative = _unitOfWork.Repository<Representative>().GetById(updateDto.Id);
            if (representative == null)
                return false;
            try
            {

                representative.Name = updateDto.Name;
                representative.UserNumber = updateDto.UserNumber;
                representative.Password = updateDto.Password;
                _unitOfWork.Complete(); return true;
            }
            catch { return false; }
        }

        #endregion

    }
}
