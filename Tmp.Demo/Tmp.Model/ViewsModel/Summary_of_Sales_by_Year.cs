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
	[Table("Summary of Sales by Year")]
	[Serializable]
    public class Summary_of_Sales_by_Year : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderID { get; set; }

		[Column("ShippedDate")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public DateTime? ShippedDate { get; set; }

		[Column("Subtotal")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal? Subtotal { get; set; }

	}
}