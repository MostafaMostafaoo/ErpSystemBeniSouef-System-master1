﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Core.Entities
{
    public class MainArea : BaseEntity
    { 
        public string Name { get; set; }
        public int StartNumbering { get; set; }
        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public virtual ICollection<SubArea>? SubAreas { get; set; }  = new List<SubArea>();
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
