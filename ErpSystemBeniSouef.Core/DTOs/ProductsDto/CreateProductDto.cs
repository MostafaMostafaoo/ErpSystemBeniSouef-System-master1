﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        public int Id { get; set; } = 0;
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public decimal CommissionRate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalePrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int CompanyId { get; set; }
    }
  
}
