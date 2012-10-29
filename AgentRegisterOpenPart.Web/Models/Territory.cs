using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AgentRegisterOpenPart.Web.Models
{
    public class Territory
    {
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Территория: длина КЛАДР-кода территории должна быть равна 13.")]
        public virtual string KLADRCode { get; set; }
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Территория: длина названия вне границ 5..100.")]
        public virtual string Name { get; set; }
    }
}