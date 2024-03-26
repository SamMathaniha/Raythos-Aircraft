using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RaythosAircraft.Data;
using RaythosAircraft.Extensions;
using RaythosAircraft.Models;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using static RaythosAircraft.Data.RaythosAircraft_db_Context;

namespace RaythosAircraft.Controllers
{
    public class ProductsController : Controller
    {
        private readonly RaythosAircraft_db_Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(RaythosAircraft_db_Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Client")]

        //Retrieve products to Aircraft_Catalog
        public async Task<IActionResult> Aircraft_Catalog()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        //Retrieve products to Commercial_aircraft page
        public async Task<IActionResult> Commercial_aircraft()
        {
            var commercialProducts = await _context.Products.Where(p => p.Category == "Commercial Aircraft").ToListAsync();
            return View(commercialProducts);
        }

        //Retrieve products to Helicopters page
        public async Task<IActionResult> Helicopters()
        {
            var commercialProducts = await _context.Products.Where(p => p.Category == "Helicopters").ToListAsync();
            return View(commercialProducts);
        }

        //Retrieve products to Defence page
        public async Task<IActionResult> Defence()
        {
            var commercialProducts = await _context.Products.Where(p => p.Category == "Defence").ToListAsync();
            return View(commercialProducts);
        }

        //Retrieve products to Gliders page
        public async Task<IActionResult> Gliders()
        {
            var commercialProducts = await _context.Products.Where(p => p.Category == "Gliders").ToListAsync();
            return View(commercialProducts);
        }

        //Retrieve products to product view page
        public async Task<IActionResult> ProductView(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            var relatedProducts = await _context.Products.Where(p => p.ProductId != id).ToListAsync();

            var purchaseOrder = HttpContext.Session.GetObjectFromJson<List<Products>>("PurchaseOrder") ?? new List<Products>();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts,
                PurchaseOrder = purchaseOrder
            };

            viewModel.Total = viewModel.Product.Basic_Price + viewModel.PurchaseOrder.Sum(p => p.Basic_Price);

            return View(viewModel);
        }

        public async Task<IActionResult> AddToPurchaseOrder(int id, int productIdToAdd)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            var relatedProducts = await _context.Products.Where(p => p.ProductId != id).ToListAsync();

            var purchaseOrder = HttpContext.Session.GetObjectFromJson<List<Products>>("PurchaseOrder") ?? new List<Products>();

            var productToAdd = await _context.Products.FindAsync(productIdToAdd);
            if (productToAdd != null)
            {
                purchaseOrder.Add(productToAdd);
                HttpContext.Session.SetObjectAsJson("PurchaseOrder", purchaseOrder);
            }

            return RedirectToAction("ProductView", new { id = id });
        }

        //Delete button for adding prices in the purchase order
        public async Task<IActionResult> DeleteFromPurchaseOrder(int id, int productIdToDelete)
        {
            var purchaseOrder = HttpContext.Session.GetObjectFromJson<List<Products>>("PurchaseOrder") ?? new List<Products>();

            var productToRemove = purchaseOrder.FirstOrDefault(p => p.ProductId == productIdToDelete);
            if (productToRemove != null)
            {
                purchaseOrder.Remove(productToRemove);
                HttpContext.Session.SetObjectAsJson("PurchaseOrder", purchaseOrder);
            }

            return RedirectToAction("ProductView", new { id = id });
        }

        //Retrieve Total Price into Order Summary
        public async Task<IActionResult> Payment()
        {
            return View();
        }


        //Insert to Details to Orders

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderSummary(string ProductName, double Total, int TotalDays)
        {
            var viewModel = new ProductDetailViewModel
            {
                Product = new Products{Name = ProductName},
                Total = Total,
                TotalDays = TotalDays  // Store the total days for delivery calculation
            };

            //Retriving User Email in the Email Input
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ViewBag.UserEmail = userEmail;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDetails(string productName, double Total, DateTime PurchaseDate, string DeliveryDate, string name, string Address, string Number, string Email, string Status, string AgreementStatus)
        {
            try
            {
                var order = new Order
                {
                    ProductName = productName,
                    Total = Total,
                    PurchaseDate = PurchaseDate,
                    DeliveryDate = DateTime.Parse(DeliveryDate),
                    Name = name,
                    Address = Address,
                    ContactNo = Number,
                    Email = Email,
                    Status = Status,
                    AgreementStatus = AgreementStatus
                };

                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                int orderId = order.OrderId;
                return Json(new { success = true, message = "Details entered successfully!", orderId = orderId });
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
                return Json(new { success = false, message = ex.InnerException?.Message ?? ex.Message });
            }
        }



        //Purchase History & Shipping
        public async Task<IActionResult> PurchaseHistory_Shipping()
        {
            // Retrieve the user's email
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            // Query the database for orders related to this user
            var orders = await _context.Order.Where(o => o.Email == userEmail).ToListAsync();

            // Create and populate the view model
            var viewModel = new ProductDetailViewModel
            {
                Orders = orders // Assuming your ViewModel has an Orders property
            };

            return View(viewModel);
        }


        /// Payment ///
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(string cardName, string cardNumber, string cardCVC, string cardExpiry, string paymentStatus, DateTime paymentDate)
        {
            try
            {
                var payment = new Payment
                {
                    cardName = cardName,
                    cardNumber = cardNumber,
                    cardCVC = cardCVC,
                    cardExpiry = cardExpiry,
                    paymentStatus = paymentStatus,
                    paymentDate = paymentDate
                };

                _context.Payment.Add(payment);
                await _context.SaveChangesAsync();

                string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                ViewBag.UserEmail = userEmail;

                int Id = payment.Id;
                return Json(new { success = true, message = "Details entered successfully!", Id = Id });
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
                return Json(new { success = false, message = ex.InnerException?.Message ?? ex.Message });
            }
        }



        ///////////////////////////////Admin side Controller/////////////////////////////////////

        [Authorize(Roles = "Admin")]
        // GET: Products
        public async Task<IActionResult> Index(string SearchBy, string search)
        {
            if (SearchBy == "Category")
            {
                return View(await _context.Products.Where(x => x.Category == search || search == null).ToListAsync());
            }
            else
            {
                return View(await _context.Products.Where(x => x.Name.StartsWith(search) || search == null).ToListAsync());

            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductCode,Name,Category,Img_1,Img_2,Basic_Price,Specifications,Description,Days,Quantity")] Products products)
        {
            // Get the web root path where the images will be stored
            string webRootPath = _webHostEnvironment.WebRootPath;

            // Get the uploaded files from the request
            var files = HttpContext.Request.Form.Files;

            // Check if exactly two files were uploaded
            if (files.Count == 2)
            {
                // Generate unique file names for each image
                string newFileName1 = Guid.NewGuid().ToString();
                string newFileName2 = Guid.NewGuid().ToString();

                // Specify the directory where the images will be stored
                var upload = Path.Combine(webRootPath, @"images\products");

                // Get file extensions for each image
                var extension1 = Path.GetExtension(files[0].FileName);
                var extension2 = Path.GetExtension(files[1].FileName);

                // Save the first uploaded image with its unique file name
                using (var fileStream1 = new FileStream(Path.Combine(upload, newFileName1 + extension1), FileMode.Create))
                {
                    files[0].CopyTo(fileStream1);
                }

                // Save the second uploaded image with its unique file name
                using (var fileStream2 = new FileStream(Path.Combine(upload, newFileName2 + extension2), FileMode.Create))
                {
                    files[1].CopyTo(fileStream2);
                }

                // Set the file paths in the Products object
                products.Img_1 = @"\images\products\" + newFileName1 + extension1;
                products.Img_2 = @"\images\products\" + newFileName2 + extension2;

                // Check if the model state is valid
                if (ModelState.IsValid)
                {
                    // Add the Products object to the context and save changes
                    _context.Add(products);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful creation
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                // Handle the case where the number of uploaded files is not as expected
                ModelState.AddModelError("", "Please upload exactly two images.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductCode,Name,Category,Img_1,Img_2,Basic_Price,Specifications,Description,Days,Quantity")] Products updatedProduct, IFormFile newImg1, IFormFile newImg2)
        {
            if (id != updatedProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);

                    if (originalProduct == null)
                    {
                        return NotFound(); // Product not found
                    }

                    var imgPath1 = await HandleImageUpload(newImg1, originalProduct.Img_1);
                    var imgPath2 = await HandleImageUpload(newImg2, originalProduct.Img_2);

                    // Update properties in the updatedProduct object
                    updatedProduct.Img_1 = imgPath1;
                    updatedProduct.Img_2 = imgPath2;


                    // Update other properties
                    _context.Update(updatedProduct);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(updatedProduct.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(updatedProduct);
        }

        private async Task<string> HandleImageUpload(IFormFile newImage, string oldImagePath)
        {
            string imagePath = oldImagePath;

            if (newImage != null)
            {
                // Delete the old image
                if (!string.IsNullOrEmpty(oldImagePath))
                {
                    var oldImagePathFull = Path.Combine(_webHostEnvironment.WebRootPath, oldImagePath.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePathFull))
                    {
                        System.IO.File.Delete(oldImagePathFull);
                    }
                }

                // Save the new image with a unique file name
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");

                using (var fileStream = new FileStream(Path.Combine(uploadPath, newFileName), FileMode.Create))
                {
                    await newImage.CopyToAsync(fileStream);
                }

                // Update the image path
                imagePath = $@"\images\products\{newFileName}";
            }

            return imagePath;
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        /*public IActionResult ProductReport()
        {
            // Retrieve the list of products from the database
            var productList = _context.Products.ToList();
 
            // Pass the list of products to the view
            return View(productList);
        }*/

        public IActionResult ProductReport(string SearchBy, string search)
        {
            // Retrieve the list of products from the database
            var productList = _context.Products.AsQueryable();

            // Pass the filtered list of products to the view
            return View(productList.ToList());
        }
    }
}
