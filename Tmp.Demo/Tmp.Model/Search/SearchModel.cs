using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Model.Search
{

    public partial class SearchOrders
    {
        public int OrderID { get; set; }// 
        public string CustomerID { get; set; }// 
        public int EmployeeID { get; set; }// 
        public DateTime OrderDate { get; set; }// 
        public DateTime RequiredDate { get; set; }// 
        public DateTime ShippedDate { get; set; }// 
        public int? ShipVia { get; set; }// 
        public string Freight { get; set; }// 
        public string ShipName { get; set; }// 
        public string ShipAddress { get; set; }// 
        public string ShipCity { get; set; }// 
        public string ShipRegion { get; set; }// 
        public string ShipPostalCode { get; set; }// 
        public string ShipCountry { get; set; }// 
    }
    public partial class SearchProducts
    {
        public int ProductID { get; set; }// 
        public string ProductName { get; set; }// 
        public int SupplierID { get; set; }// 
        public int CategoryID { get; set; }// 
        public string QuantityPerUnit { get; set; }// 
        public string UnitPrice { get; set; }// 
        public int UnitsInStock { get; set; }// 
        public int UnitsOnOrder { get; set; }// 
        public int ReorderLevel { get; set; }// 
        public bool Discontinued { get; set; }// 
    }
    public partial class SearchOrderDetails
    {
        public int OrderID { get; set; }// 
        public int ProductID { get; set; }// 
        public string UnitPrice { get; set; }// 
        public int Quantity { get; set; }// 
        public string Discount { get; set; }// 
    }
    public partial class SearchCustomerCustomerDemo
    {
        public string CustomerID { get; set; }// 
        public string CustomerTypeID { get; set; }// 
    }
    public partial class SearchCustomerDemographics
    {
        public string CustomerTypeID { get; set; }// 
        public string CustomerDesc { get; set; }// 
    }
    public partial class SearchRegion
    {
        public int RegionID { get; set; }// 
        public string RegionDescription { get; set; }// 
    }
    public partial class SearchTerritories
    {
        public string TerritoryID { get; set; }// 
        public string TerritoryDescription { get; set; }// 
        public int RegionID { get; set; }// 
    }
    public partial class SearchEmployeeTerritories
    {
        public int EmployeeID { get; set; }// 
        public string TerritoryID { get; set; }// 
    }
    public partial class Searchpro
    {
        public int proid { get; set; }// 
        public string proname { get; set; }// 
        public int unitsinstock { get; set; }// 
    }
    public partial class SearchEmployees
    {
        public int EmployeeID { get; set; }// 
        public string LastName { get; set; }// 
        public string FirstName { get; set; }// 
        public string Title { get; set; }// 
        public string TitleOfCourtesy { get; set; }// 
        public DateTime BirthDate { get; set; }// 
        public DateTime HireDate { get; set; }// 
        public string Address { get; set; }// 
        public string City { get; set; }// 
        public string Region { get; set; }// 
        public string PostalCode { get; set; }// 
        public string Country { get; set; }// 
        public string HomePhone { get; set; }// 
        public string Extension { get; set; }// 
        public string Photo { get; set; }// 
        public string Notes { get; set; }// 
        public int ReportsTo { get; set; }// 
        public string PhotoPath { get; set; }// 
    }
    public partial class SearchCategories
    {
        public int CategoryID { get; set; }// 
        public string CategoryName { get; set; }// 
        public string Description { get; set; }// 
        public string Picture { get; set; }// 
    }
    public partial class SearchCustomers
    {
        public string CustomerID { get; set; }// 
        public string CompanyName { get; set; }// 
        public string ContactName { get; set; }// 
        public string ContactTitle { get; set; }// 
        public string Address { get; set; }// 
        public string City { get; set; }// 
        public string Region { get; set; }// 
        public string PostalCode { get; set; }// 
        public string Country { get; set; }// 
        public string Phone { get; set; }// 
        public string Fax { get; set; }// 
    }
    public partial class SearchShippers
    {
        public int ShipperID { get; set; }// 
        public string CompanyName { get; set; }// 
        public string Phone { get; set; }// 
    }
    public partial class SearchSuppliers
    {
        public int SupplierID { get; set; }// 
        public string CompanyName { get; set; }// 
        public string ContactName { get; set; }// 
        public string ContactTitle { get; set; }// 
        public string Address { get; set; }// 
        public string City { get; set; }// 
        public string Region { get; set; }// 
        public string PostalCode { get; set; }// 
        public string Country { get; set; }// 
        public string Phone { get; set; }// 
        public string Fax { get; set; }// 
        public string HomePage { get; set; }// 
    }
}
