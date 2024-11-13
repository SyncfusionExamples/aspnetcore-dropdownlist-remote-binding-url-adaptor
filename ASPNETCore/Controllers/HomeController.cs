using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore.Models;

namespace ASPNETCore.Controllers
{
    public class HomeController : Controller
    {
        OrdersDetails model = new OrdersDetails();
        public ActionResult Index()
        {
            model.ID = 1;
            model.Values = new List<long>() { 1, 2, 3 };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(OrdersDetails model)
        {
            model.ID = model.ID;
            model.Values = model.Values;
            return View(model);
        }
        public ActionResult UrlDatasource(OrdersDetails model, [FromBody] Data dm)
        {
            var val = OrdersDetails.GetAllRecords(); // You need to bind the sql data here
            var Data = val.ToList();
            var count = val.Count();
            if (dm.where != null)
            {
                Data = (from cust in Data
                        where cust.ShipCountry.ToLower().StartsWith(dm.@where[0].value.ToString())
                        select cust).ToList();
            }
            if (dm.take != 0)
                Data = Data.Take(dm.take).ToList();
            return Json(Data);
        }


        public class Data
        {
            public int take { get; set; }
            public List<Wheres> where { get; set; }
        }

        public class Wheres
        {
            public string field { get; set; }
            public bool ignoreAccent { get; set; }

            public bool ignoreCase { get; set; }

            public bool isComplex { get; set; }

            public string value { get; set; }
            public string Operator { get; set; }

        }
        public class OrdersDetails
        {
            public static List<OrdersDetails> order = new List<OrdersDetails>();
            public OrdersDetails()
            {

            }
            public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
            {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.Verified = Verified;
                this.OrderDate = OrderDate;
                this.ShipName = ShipName;
                this.ShipCountry = ShipCountry;
                this.ShippedDate = ShippedDate;
                this.ShipAddress = ShipAddress;
            }
            public static List<OrdersDetails> GetAllRecords()
            {
                if (order.Count() == 0)
                {

                    order.Add(new OrdersDetails(1, "ALFKI", 0, 2.3, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", "Denmark", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                    order.Add(new OrdersDetails(2, "ANATR", 2, 3.3, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                    order.Add(new OrdersDetails(3, "ANTON", 1, 4.3, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                    order.Add(new OrdersDetails(4, "BLONP", 3, 5.3, false, new DateTime(1930, 10, 22), "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                    order.Add(new OrdersDetails(5, "BOLID", 4, 6.3, true, new DateTime(1953, 02, 18), "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                }
                return order;
            }

            public long? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime OrderDate { get; set; }

            public string ShipName { get; set; }

            public string ShipCountry { get; set; }

            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }

            public List<long> Values { get; set; }
            public int? ID { get; set; }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
