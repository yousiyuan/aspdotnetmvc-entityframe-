using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Model;

namespace Tmp.DAL
{
    public class CoreDataContext : BaseContext<CoreDataContext>
    {
        public CoreDataContext() : base("TmpConnStr") { }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<pro> pro { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Territories> Territories { get; set; }


        //视图
        public DbSet<Current_Product_List> Current_Product_List { get; set; }
        public DbSet<Alphabetical_list_of_products> Alphabetical_list_of_products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除复数表名的契约
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            
            
            //在数据库表OrderDetails中，字段OrderID与ProductID是复合主键
            modelBuilder.Entity<OrderDetails>().HasKey(t => new { t.OrderID, t.ProductID });

            //在数据库表EmployeeTerritories中，字段EmployeeID与TerritoryID是复合主键
            modelBuilder.Entity<EmployeeTerritories>().HasKey(t => new { t.EmployeeID, t.TerritoryID });

            //在数据库表CustomerCustomerDemo中，字段CustomerID与CustomerTypeID是复合主键
            modelBuilder.Entity<CustomerCustomerDemo>().HasKey(t => new { t.CustomerID, t.CustomerTypeID });


            //视图，设置复合主键
            modelBuilder.Entity<Alphabetical_list_of_products>().HasKey(t => new { t.CategoryID, t.ProductID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
