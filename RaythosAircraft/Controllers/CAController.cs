using Microsoft.AspNetCore.Mvc;
using RaythosAircraft.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace RaythosAircraft.Controllers
{
    [Authorize(Roles = "ControlAssistant")]
    public class CAController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Order> OrderLists = new List<Order>();


        private readonly ILogger<CAController> _logger;

        public CAController(ILogger<CAController> logger)
        {
            _logger = (ILogger<CAController>)logger;
            con.ConnectionString = RaythosAircraft.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderStage()
        {
            FetchData();
            return View(OrderLists);
        }

        public IActionResult ProductionStage()
        {
            FetchDataProduction();
            return View(OrderLists);
        }

        public IActionResult DeliveryStage()
        {
            FetchDataDelivery();
            return View(OrderLists);
        }

        public IActionResult CompletedStage()
        {
            FetchDataCompleted();
            return View(OrderLists);
        }

        public IActionResult Report_CompletedStage()
        {
            // Action logic here
            FetchDataCompleted();
            return View(OrderLists);
        }


        // Index order stage
        private void FetchData()
        {
            if (OrderLists.Count > 0)
            {
                OrderLists.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [OrderId]      ,[Name]      ,[Email]      ,[Address]      ,[ContactNo]      ,[AgreementStatus]      ,[PurchaseDate]      ,[DeliveryDate]      ,[Status]      ,[Total]      ,[ProductName]  FROM [RaythosAircraft].[dbo].[Order] WHERE [Status] = 'Placed'";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    OrderLists.Add(new Order()
                    {
                        OrderId = Convert.ToInt32(dr["OrderId"])
                    ,
                        Name = dr["Name"].ToString()
                    ,
                        Email = dr["Email"].ToString()
                    ,
                        Address = dr["Address"].ToString()
                    ,
                        ContactNo = dr["ContactNo"].ToString()
                    ,
                        AgreementStatus = dr["AgreementStatus"].ToString()
                    ,
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"])
                    ,
                        DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"])
                    ,
                        Status = dr["Status"].ToString()
                    ,
                        Total = Convert.ToDouble(dr["Total"])
                    ,
                        ProductName = dr["ProductName"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // prodcution stage
        private void FetchDataProduction()
        {
            if (OrderLists.Count > 0)
            {
                OrderLists.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [OrderId]      ,[Name]      ,[Email]      ,[Address]      ,[ContactNo]      ,[AgreementStatus]      ,[PurchaseDate]      ,[DeliveryDate]      ,[Status]      ,[Total]      ,[ProductName]  FROM [RaythosAircraft].[dbo].[Order] WHERE [Status] = 'Production'";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    OrderLists.Add(new Order()
                    {
                        OrderId = Convert.ToInt32(dr["OrderId"])
                    ,
                        Name = dr["Name"].ToString()
                    ,
                        Email = dr["Email"].ToString()
                    ,
                        Address = dr["Address"].ToString()
                    ,
                        ContactNo = dr["ContactNo"].ToString()
                    ,
                        AgreementStatus = dr["AgreementStatus"].ToString()
                    ,
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"])
                    ,
                        Status = dr["Status"].ToString()
                    ,
                        Total = Convert.ToDouble(dr["Total"])
                    ,
                        ProductName = dr["ProductName"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Delivery Stage
        private void FetchDataDelivery()
        {
            if (OrderLists.Count > 0)
            {
                OrderLists.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [OrderId]      ,[Name]      ,[Email]      ,[Address]      ,[ContactNo]      ,[AgreementStatus]      ,[PurchaseDate]      ,[DeliveryDate]      ,[Status]      ,[Total]      ,[ProductName]  FROM [RaythosAircraft].[dbo].[Order]  WHERE [Status] = 'Delivery'";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    OrderLists.Add(new Order()
                    {
                        OrderId = Convert.ToInt32(dr["OrderId"])
                   ,
                        Name = dr["Name"].ToString()
                   ,
                        Email = dr["Email"].ToString()
                   ,
                        Address = dr["Address"].ToString()
                   ,
                        ContactNo = dr["ContactNo"].ToString()
                   ,
                        AgreementStatus = dr["AgreementStatus"].ToString()
                   ,
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"])
                   ,
                        Status = dr["Status"].ToString()
                    ,
                        Total = Convert.ToDouble(dr["Total"])
                    ,
                        ProductName = dr["ProductName"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        // Completed stage
        private void FetchDataCompleted()
        {
            if (OrderLists.Count > 0)
            {
                OrderLists.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [OrderId]      ,[Name]      ,[Email]      ,[Address]      ,[ContactNo]      ,[AgreementStatus]      ,[PurchaseDate]      ,[DeliveryDate]     ,[Status]      ,[Total]      ,[ProductName]  FROM [RaythosAircraft].[dbo].[Order] WHERE [Status] = 'Completed'";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    OrderLists.Add(new Order()
                    {
                        OrderId = Convert.ToInt32(dr["OrderId"])
                   ,
                        Name = dr["Name"].ToString()
                   ,
                        Email = dr["Email"].ToString()
                   ,
                        Address = dr["Address"].ToString()
                   ,
                        ContactNo = dr["ContactNo"].ToString()
                   ,
                        AgreementStatus = dr["AgreementStatus"].ToString()
                   ,
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"])
                   ,
                        Status = dr["Status"].ToString()
                    ,
                        Total = Convert.ToDouble(dr["Total"])
                    ,
                        ProductName = dr["ProductName"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // index update
        public IActionResult UpdateToProduction(string orderId)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = $"UPDATE [RaythosAircraft].[dbo].[Order] SET [Status] = 'Production' WHERE [OrderId] = '{orderId}'";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, e.g., log it or show an error message
                throw ex;
            }

            // Redirect back to the Index action after the update
            return RedirectToAction("OrderStage");
        }



        // Production update
        public IActionResult UpdateToDelivery(string orderId)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = $"UPDATE [RaythosAircraft].[dbo].[Order] SET [Status] = 'Delivery' WHERE [OrderId] = '{orderId}'";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, e.g., log it or show an error message
                throw ex;
            }

            // Redirect back to the Index action after the update
            return RedirectToAction("ProductionStage");
        }


        // Delivey update
        public IActionResult UpdateToCompleted(string orderId)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = $"UPDATE [RaythosAircraft].[dbo].[Order] SET [Status] = 'Completed' WHERE [OrderId] = '{orderId}'";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, e.g., log it or show an error message
                throw ex;
            }

            // Redirect back to the Index action after the update
            return RedirectToAction("DeliveryStage");
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



