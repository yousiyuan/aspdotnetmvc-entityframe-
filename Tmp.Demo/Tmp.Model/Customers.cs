//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//       生成时间 2017-02-10 12:22:57 by 诸葛冷冷
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Tmp.Common.Tools.Helper;

namespace Tmp.Model
{
    [Table("Customers")]
    [Serializable]
    public class Customers : BaseEntity
    {
        
        [Key]
        [Column("CustomerID")]
        [StringLength(5, ErrorMessage = "{0}不能超过5个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string CustomerID { get; set; }
        
        [Column("CompanyName")]
        [StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string CompanyName { get; set; }
        
        [Column("ContactName")]
        [StringLength(30, ErrorMessage = "{0}不能超过30个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ContactName { get; set; }
        
        [Column("ContactTitle")]
        [StringLength(30, ErrorMessage = "{0}不能超过30个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ContactTitle { get; set; }
        
        [Column("Address")]
        [StringLength(60, ErrorMessage = "{0}不能超过60个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Address { get; set; }
        
        [Column("City")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string City { get; set; }
        
        [Column("Region")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Region { get; set; }
        
        [Column("PostalCode")]
        [StringLength(10, ErrorMessage = "{0}不能超过10个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string PostalCode { get; set; }
        
        [Column("Country")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Country { get; set; }
        
        [Column("Phone")]
        [StringLength(24, ErrorMessage = "{0}不能超过24个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Phone { get; set; }
        
        [Column("Fax")]
        [StringLength(24, ErrorMessage = "{0}不能超过24个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Fax { get; set; }

        /// <summary>
        /// 属性浅拷贝
        /// </summary>
        /// <returns></returns>
        public Customers Clone()
        {
            return ObjectCopier.Clone<Customers>(this);
        }
    }
}
