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
	[Table("Invoices")]
	[Serializable]
    public class Invoices : BaseEntity
	{
		[Column("Address")]
		[StringLength(60, ErrorMessage = "{0}不能超过60个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string Address { get; set; }

		[Column("City")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string City { get; set; }

		[Column("Country")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string Country { get; set; }

		//复合主键需要在OnModelCreating中手动定义
		[Column("CustomerID")]
		[StringLength(5, ErrorMessage = "{0}不能超过5个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string CustomerID { get; set; }

		[Column("CustomerName")]
		[StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string CustomerName { get; set; }

		[Column("Discount")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public Single Discount { get; set; }

		[Column("ExtendedPrice")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal? ExtendedPrice { get; set; }

		[Column("Freight")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal? Freight { get; set; }

		[Column("OrderDate")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public DateTime? OrderDate { get; set; }

		//复合主键需要在OnModelCreating中手动定义
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderID { get; set; }

		[Column("PostalCode")]
		[StringLength(10, ErrorMessage = "{0}不能超过10个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string PostalCode { get; set; }

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

		[Column("Region")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string Region { get; set; }

		[Column("RequiredDate")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public DateTime? RequiredDate { get; set; }

		[Column("Salesperson")]
		[StringLength(31, ErrorMessage = "{0}不能超过31个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string Salesperson { get; set; }

		[Column("ShipAddress")]
		[StringLength(60, ErrorMessage = "{0}不能超过60个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipAddress { get; set; }

		[Column("ShipCity")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipCity { get; set; }

		[Column("ShipCountry")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipCountry { get; set; }

		[Column("ShipName")]
		[StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipName { get; set; }

		[Column("ShippedDate")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public DateTime? ShippedDate { get; set; }

		[Column("ShipperName")]
		[StringLength(40, ErrorMessage = "{0}不能超过40个字符")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipperName { get; set; }

		[Column("ShipPostalCode")]
		[StringLength(10, ErrorMessage = "{0}不能超过10个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipPostalCode { get; set; }

		[Column("ShipRegion")]
		[StringLength(15, ErrorMessage = "{0}不能超过15个字符")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public string ShipRegion { get; set; }

		[Column("UnitPrice")]
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "字段中文名称，实际使用时，需改")]
		public decimal UnitPrice { get; set; }

	}
}
