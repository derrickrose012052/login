using MVC_LogoIn.Models;
using MVC_LogoIn.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MVC_LogoIn.Controllers
{
    public class isLogoInController : Controller
    {      
        // GET: is首頁
        public ActionResult HomeList()
        {
           
           CustomersFactory cu = new CustomersFactory();
            if (Session[ReadonlySession.sk_customers_LogoIn_Email_Account]==null)
            {
                return RedirectToAction("LogoIn");
            }
            return View(cu.getAll());
        }

        [HttpPost]
        public ActionResult HomeList(string searching)
        {
            CCustomers[] cust1 = new CustomersFactory().getByKeyword(searching);
            if(cust1==null)
            {               
               return RedirectToAction("HomeList");
            }
            return View(cust1);
        }
        public ActionResult LogoIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogoIn(LogoInCustomers1 p)
        {
            CCustomers cust1 = new CustomersFactory().getEmail(p.txtEmail);
            if(cust1!=null)
            {
                if (cust1.Password.Equals(p.txtPassword))
                {
                    Session[ReadonlySession.sk_customers_LogoIn_Email_Account] = cust1.UserName;
                    return RedirectToAction("HomeList");
                }
            }
            return RedirectToAction("LogoIn");
        }
        public ActionResult insert12()
        {
            return View();
        }
        [HttpPost]
        public ActionResult insert12(CCustomers p)
        {
            new CustomersFactory().inserts(p);
            return RedirectToAction("HomeList");
        }

        public ActionResult update12(int id)
        {
            Customers update = new LogoInCustomersEntities().Customers.First(p => p.fid == id);
            if (update == null)
                return RedirectToAction("HomeList");
            return View(update);
        }

        [HttpPost]
        public ActionResult update12(Customers p)
        {
            LogoInCustomersEntities a = new LogoInCustomersEntities();
            Customers update1 = a.Customers.First(s => s.fid == p.fid);
            if(update1==null)
            {
                return RedirectToAction("HomeList");
            }
            update1.femail = p.femail;
            update1.fusername = p.fusername;
            update1.fgender = p.fgender;
            update1.faddress = p.faddress;
            update1.fphone = p.fphone;
            update1.fphoto = p.fphoto;
            a.SaveChanges();
            return RedirectToAction("HomeList");
        }
    
        public ActionResult delete12(int id)
        {
            LogoInCustomersEntities del = new LogoInCustomersEntities();
            Customers customer = del.Customers.First(s=>s.fid==id);
            del.Customers.Remove(customer);
            del.SaveChanges();
            return RedirectToAction("HomeList");
         }
        //==============購物車=======================//

        //public ActionResult shop()
        //{
        //    var s = from p in (new LogoInCustomersEntities()).Products
        //            select p;
        //    return View(s);
        //}

        //public ActionResult AddCarShop(int id)
        //{
        //    Product products = new LogoInCustomersEntities().Products.First(p => p.pid == id);
        //    if(products==null)
        //    {
        //        return RedirectToAction("shop");
        //    }
        //    return View(products);
        //}
        //[HttpPost]
        //public ActionResult AddCarShop(Shopping item)
        //{
        //    LogoInCustomersEntities shop = new LogoInCustomersEntities();
        //    int productid = Convert.ToInt32(item.scustomername);
        //    Product product1 = shop.Products.FirstOrDefault(p=>p.pid==productid);
        //    if(product1!=null)
        //    {
        //        item.sdate = DateTime.Now.ToString("yyyyhhddmmss");
        //        item.scustomername = "Jeo";
        //        item.sprice = product1.pprice;
        //        shop.Shoppings.Add(item);
        //        shop.SaveChanges();
        //    }
        //    return RedirectToAction("shop");
        //}

        //public ActionResult AddCarSession(int id)
        //{
        //    Product sessionproducts = new LogoInCustomersEntities().Products.First(p => p.pid == id);
        //    if(sessionproducts==null)
        //    {
        //        return RedirectToAction("shop");
        //    }
        //    return View(sessionproducts);
        //}
        //[HttpPost]
        //public ActionResult AddCarSession(Shopping p)
        //{
        //    LogoInCustomersEntities shopsession = new LogoInCustomersEntities();
        //    int productid = Convert.ToInt32(p.sproductname);
        //    Product product1 = shopsession.Products.FirstOrDefault(s => s.pid == productid);
        //    if (product1 != null)
        //    {
        //        List<shoppingcar> list = (List<shoppingcar>)Session[ReadonlySession.sk_products_shopping_car];
        //        if (list==null)
        //        {
        //            list = new List<shoppingcar>();
        //            Session[ReadonlySession.sk_products_shopping_car] = list;
        //        }
        //        shoppingcar x = new shoppingcar();
        //        x.pPrice = Convert.ToDouble(product1.pprice);
        //        x.ProductId = product1.pid;
        //        x.ProductName = product1.pproductname;
        //        x.pCount = (int)p.scount;
        //        list.Add(x);
        //    }
        //    return RedirectToAction("shop");
        //}

        //public ActionResult shopCarList()
        //{
        //    List<shoppingcar> list1 = new List<shoppingcar>();
        //    list1 = Session[ReadonlySession.sk_products_shopping_car] as List<shoppingcar>;
        //    if (list1 == null)
        //        return RedirectToAction("shop");
        //    return View(list1);

        //}
    }
}