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
	[Table("Order Details Extended")]
	[Serializable]
    public class Order_Details_Extended : BaseEntity
	{
		[Column("Discount")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public Single Discount { get; set; }

		[Column("ExtendedPrice")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal? ExtendedPrice { get; set; }

		//复合主键需要在OnModelCreating中手动定义
		[Column("OrderID")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public int OrderID { get; set; }

		//复合主键需要在OnModelCreating中手动定义
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductID { get; set; }

		[Column("ProductName")]
		[StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ProductName { get; set; }

		[Column("Quantity")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public short Quantity { get; set; }

		[Column("UnitPrice")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal UnitPrice { get; set; }

	}
}
