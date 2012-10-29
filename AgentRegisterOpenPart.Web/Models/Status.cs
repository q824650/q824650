using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AgentRegisterOpenPart.Web.Models
{
    public class Status
    {
        public virtual int Id { get; set; }
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Статус: длина названия вне границ 5..60.")]
        public virtual string Name { get; set; }
    }
}