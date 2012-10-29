using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AgentRegisterOpenPart.Web.Models
{
    public class InsuranceCompany
    {
        public virtual int Id { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Страховая компания: длина названия вне границ 1..255.")]
        public virtual string Name { get; set; }
    }
}