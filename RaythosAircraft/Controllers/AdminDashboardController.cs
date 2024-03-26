using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaythosAircraft.Data;
using RaythosAircraft.Models;

namespace RaythosAircraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly RaythosAircraft_db_Context _context;

        public AdminDashboardController(RaythosAircraft_db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve counts from the database
            int productCount = _context.Products.Count();
            int inventoryCount = _context.Inventory.Count();
            int orderCount = _context.Orders.Count();
            //int paymentCount = _context.Payment.Count();

            // Create an instance of CountPanel and set the counts
            CountPanel countPanel = new CountPanel
            {
                ProductCount = productCount,
                InventoryCount = inventoryCount,
                // Set properties for other counts
                OrderCount = orderCount,
                //PaymentCount = paymentCount
            };

            // Pass the CountPanel instance to the view
            return View(countPanel);
        }
    }
}
