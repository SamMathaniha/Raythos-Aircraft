using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaythosAircraft.Data;
using RaythosAircraft.Models;

namespace RaythosAircraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoriesController : Controller
    {
        private readonly RaythosAircraft_db_Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InventoriesController(RaythosAircraft_db_Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Inventory
        public async Task<IActionResult> Index(string SearchBy, string search)
        {
            if (SearchBy == "Category")
            {
                return View(await _context.Inventory.Where(x => x.Category == search || search == null).ToListAsync());
            }
            else
            {
                return View(await _context.Inventory.Where(x => x.Name.StartsWith(search) || search == null).ToListAsync());

            }
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
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
        public async Task<IActionResult> Create([Bind("InventoryId,ItemCode,Name,Category,Img1,Price,Specifications,Description,Type,Quality,Days,Quantity")] Inventory inventory, IFormFile img1)
        {
            // Check if an image was uploaded
            if (img1 != null && img1.Length > 0)
            {
                // Generate a unique file name for the image
                string newFileName1 = Guid.NewGuid().ToString();

                // Specify the directory where the images will be stored
                var upload = Path.Combine(_webHostEnvironment.WebRootPath, @"InventoryImage\inventories");

                // Get the file extension for the uploaded image
                var extension1 = Path.GetExtension(img1.FileName);

                // Save the uploaded image with its unique file name
                using (var fileStream1 = new FileStream(Path.Combine(upload, newFileName1 + extension1), FileMode.Create))
                {
                    img1.CopyTo(fileStream1);
                }

                // Set the file path in the Inventory object
                inventory.Img1 = "/InventoryImage/inventories/" + newFileName1 + extension1;
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the Inventory object to the context and save changes
                    _context.Add(inventory);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful creation
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    ModelState.AddModelError("", "An error occurred while saving the inventory item.");
                }
            }

            // If ModelState is not valid, return the view with the model to display validation errors
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        /*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,ItemCode,Name,Category,Img1,Price,Specifications,Description,Type,Quality,Days,Quantity")] Inventory updatedInventory, IFormFile newImg1)
        {
            if (id != updatedInventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalInventory = await _context.Inventory.AsNoTracking().FirstOrDefaultAsync(p => p.InventoryId == id);

                    if (originalInventory == null)
                    {
                        return NotFound(); // Product not found
                    }

                    var imgPath1 = await HandleImageUpload(newImg1, originalInventory.Img1);

                    // Update properties in the updatedProduct object
                    updatedInventory.Img1 = imgPath1;



                    // Update other properties
                    _context.Update(updatedInventory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(updatedInventory.InventoryId))
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
        }*/

        // GET: Iventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult InventoryReport(string SearchBy, string search)
        {
            // Retrieve the list of products from the database
            var productList = _context.Inventory.AsQueryable();

            // Pass the filtered list of products to the view
            return View(productList.ToList());
        }
    }
}