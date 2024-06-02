using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Domain.Entities
{
    public class SysParam
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("ParamName")]
        public string ParamName { get; set; }
        [Required]
        [Column("ParamValue")]
        public bool ParamValue { get; set; }
    }
}
