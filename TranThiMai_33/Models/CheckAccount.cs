using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TranThiMai_33.Models
{
    [Table("CheckAccounts")]
    public class CheckAccount
    {
        [Key]
        [Required(ErrorMessage = "Usename is requied.")]
        public string CheckUsername { get; set; }
        [Required(ErrorMessage = "Password is requied.")]
        [DataType(DataType.Password)]
        public string CheckPassword { get; set; }
    }
}