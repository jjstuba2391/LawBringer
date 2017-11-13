using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using LawBringer.Models;


namespace LawBringer.Controllers
{
    public class ReportsController : Controller
    {
        
        public ActionResult Index(string selectedState = "California")
        {          
            SalesReportModel model = new SalesReportModel();

           
            string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {     
                connection.Open();
            
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT StateProvince FROM Address INNER JOIN SalesOrderHeader ON Address.AddressID = SalesOrderHeader.BillToAddressID";
                
                List<string> states = new List<string>();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        states.Add(reader.GetString(0));
                    }
                }

                SqlCommand command2 = connection.CreateCommand();
                command.CommandText = @"select top 5 ProductID, SUM(LineTotal) from salesorderdetail JOIN SalesOrderHeader
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN[Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                         WHERE Address.StateProvince = '" +selectedState+ "' group by ProductID order by sum(LineTotal) DESC";

                List<TopSaleByDollar> TopSalesByDollar = new List<TopSaleByDollar>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TopSalesByDollar.Add(new TopSaleByDollar { ProductID = reader.GetInt32(0), Price = reader.GetDecimal(1) } );
                    }
                }

                SqlCommand command3 = connection.CreateCommand();
                command.CommandText = @"select top 5 ProductID, SUM(OrderQty) from salesorderdetail JOIN SalesOrderHeader
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN[Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                         WHERE Address.StateProvince = '" + selectedState + "' group by ProductID order by sum(OrderQty) DESC";

                List<TopSaleByQuantity> TopSalesByQuantity = new List<TopSaleByQuantity>();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        TopSalesByQuantity.Add(new TopSaleByQuantity {  ProductID = reader.GetInt32(0), Quantity = reader.GetInt32(1)  }  );
                    }
                }

                model.States = states.ToArray();
                model.TopSalesByDollar = TopSalesByDollar.ToArray();
                model.TopSalesByQuantity = TopSalesByQuantity.ToArray();
                connection.Close();
            }
            return View(model);
        }
    }
}