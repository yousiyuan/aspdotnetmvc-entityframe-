using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tmp.Common.Tools.Extension;
using Tmp.Common.Tools.Log;
using Tmp.DAL;
using Tmp.Facade;
using Tmp.FacadeInterface;
using Tmp.Model;

namespace Tmp.Demo.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        UnitOfWork<Customers> _cust = null;
        UnitOfWork<Categories> _cate = null;
        UnitOfWork<Region> _region = null;
        UnitOfWork<Shippers> _ship = null;
        UnitOfWork<Suppliers> _supp = null;
        UnitOfWork<CustomerDemographics> _demo = null;
        UnitOfWork<Employees> _emp = null;

        UnitOfWork<Orders> _odr = null;

        IOrdersFO odrFO = null;
        ICustomersFO cutFO = null;

        UnitOfWork<OrderDetails> _od = null;
        UnitOfWork<EmployeeTerritories> _empter = null;
        UnitOfWork<CustomerCustomerDemo> _custdmo = null;

        UnitOfWork<pro> _pro = null;

        UnitOfWork<Current_Product_List> _pdtlist = null;//视图
        UnitOfWork<Alphabetical_list_of_products> _alph = null;

        public HomeController()
        {
            _cust = new UnitOfWork<Customers>();
            _cate = new UnitOfWork<Categories>();
            _region = new UnitOfWork<Region>();
            _ship = new UnitOfWork<Shippers>();
            _supp = new UnitOfWork<Suppliers>();
            _demo = new UnitOfWork<CustomerDemographics>();
            _emp = new UnitOfWork<Employees>();

            _odr = new UnitOfWork<Orders>();

            odrFO = new OrdersFO();
            cutFO = new CustomersFO();

            _od = new UnitOfWork<OrderDetails>();//复合主键测试[附带外键的测试]
            _empter = new UnitOfWork<EmployeeTerritories>();//可以不需要声明外键关系
            _custdmo = new UnitOfWork<CustomerCustomerDemo>();//可以不需要声明外键关系

            _pro = new UnitOfWork<pro>();

            _pdtlist = new UnitOfWork<Current_Product_List>();//视图
            _alph = new UnitOfWork<Alphabetical_list_of_products>();//视图
        }

        public ActionResult Index()
        {
            var custlist = _cust.Repository.GetAll().ToList();
            var catelist = _cate.Repository.GetAll().ToList();
            var regionlist = _region.Repository.GetAll().ToList();
            var shiplist = _ship.Repository.GetAll().ToList();
            var supplist = _supp.Repository.GetAll().ToList();
            var demolist = _demo.Repository.GetAll().ToList();
            var emplist = _emp.Repository.GetAll().ToList();

            var odrlist = _odr.Repository.GetAll().ToList();

            Orders ord = odrFO.Get(new Orders { OrderID = 10252 });
            Customers cut = cutFO.Get(new Customers { CustomerID = "BLONP" });

            OrderDetails od = _od.Repository.GetAll().FirstOrDefault(t => t.OrderID == 10250 && t.ProductID == 65);
            var result = _empter.Repository.GetAll().ToList();

            var result1 = _custdmo.Repository.GetAll().ToList();

            var result2 = _pro.Repository.GetAll();

            LogHelper.Write(result2.ToString());

            Customers person1 = cut.Clone();//克隆一个副本

            var result3 = _pdtlist.Repository.GetAll();//视图
            var result4 = _alph.Repository.GetAll();//视图

            return View();
        }
    }
}
