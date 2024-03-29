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
    [Table("Products")]
    [Serializable]
    public class Products : BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        
        [Column("ProductName")]
        [StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string ProductName { get; set; }
        
        [Column("SupplierID")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public int? SupplierID { get; set; }
        
        [Column("CategoryID")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public int? CategoryID { get; set; }
        
        [Column("QuantityPerUnit")]
        [StringLength(20, ErrorMessage = "{0}不能超过20个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string QuantityPerUnit { get; set; }
        
        [Column("UnitPrice")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public decimal? UnitPrice { get; set; }
        
        [Column("UnitsInStock")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public short? UnitsInStock { get; set; }
        
        [Column("UnitsOnOrder")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public short? UnitsOnOrder { get; set; }
        
        [Column("ReorderLevel")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public short? ReorderLevel { get; set; }
        
        [Column("Discontinued")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public bool Discontinued { get; set; }

        /// <summary>
        /// 属性浅拷贝
        /// </summary>
        /// <returns></returns>
        public Products Clone()
        {
            return ObjectCopier.Clone<Products>(this);
        }
    }
}
