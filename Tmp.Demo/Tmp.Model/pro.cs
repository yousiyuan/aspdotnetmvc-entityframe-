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
    [Table("pro")]
    [Serializable]
    public class pro : BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int proid { get; set; }
        
        [Column("proname")]
        [StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public string proname { get; set; }
        
        [Column("unitsinstock")]
        [Display(Name = "字段中文名称，实际使用时，需改")]
        public int? unitsinstock { get; set; }

        /// <summary>
        /// 属性浅拷贝
        /// </summary>
        /// <returns></returns>
        public pro Clone()
        {
            return ObjectCopier.Clone<pro>(this);
        }
    }
}
