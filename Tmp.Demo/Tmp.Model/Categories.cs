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
    [Table("Categories")]
    [Serializable]
    public class Categories : BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        
        [Column("CategoryName")]
        [StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string CategoryName { get; set; }
        
        [Column("Description")]
        [StringLength(8, ErrorMessage = "{0}不能超过8个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string Description { get; set; }
        
        [Column("Picture")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public byte[] Picture { get; set; }

        /// <summary>
        /// 属性浅拷贝
        /// </summary>
        /// <returns></returns>
        public Categories Clone()
        {
            return ObjectCopier.Clone<Categories>(this);
        }
    }
}