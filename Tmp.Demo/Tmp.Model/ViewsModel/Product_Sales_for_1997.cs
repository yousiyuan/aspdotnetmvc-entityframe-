//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//       生成时间 2017-02-10 19:58:39 by 诸葛冷冷
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

namespace Tmp.Model
{
	[Table("Product Sales for 1997")]
	[Serializable]
    public class Product_Sales_for_1997 : BaseEntity
	{
		[Column("CategoryName")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string CategoryName { get; set; }

		[Column("ProductName")]
		[StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ProductName { get; set; }

		[Column("ProductSales")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal? ProductSales { get; set; }

	}
}