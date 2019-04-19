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
    [Table("Orders")]
    [Serializable]
    public class Orders : BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        
        [Column("CustomerID")]
        [StringLength(5, ErrorMessage = "{0}不能超过5个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string CustomerID { get; set; }
        
        [Column("EmployeeID")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public int? EmployeeID { get; set; }
        
        [Column("OrderDate")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public DateTime? OrderDate { get; set; }
        
        [Column("RequiredDate")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public DateTime? RequiredDate { get; set; }
        
        [Column("ShippedDate")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public DateTime? ShippedDate { get; set; }
        
        [Column("ShipVia")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public int? ShipVia { get; set; }
        
        [Column("Freight")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public decimal? Freight { get; set; }
        
        [Column("ShipName")]
        [StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipName { get; set; }
        
        [Column("ShipAddress")]
        [StringLength(60, ErrorMessage = "{0}不能超过60个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipAddress { get; set; }
        
        [Column("ShipCity")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipCity { get; set; }
        
        [Column("ShipRegion")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipRegion { get; set; }
        
        [Column("ShipPostalCode")]
        [StringLength(10, ErrorMessage = "{0}不能超过10个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipPostalCode { get; set; }
        
        [Column("ShipCountry")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ShipCountry { get; set; }

        /// <summary>
        /// 属性浅拷贝
        /// </summary>
        /// <returns></returns>
        public Orders Clone()
        {
            return ObjectCopier.Clone<Orders>(this);
        }
    }
}
