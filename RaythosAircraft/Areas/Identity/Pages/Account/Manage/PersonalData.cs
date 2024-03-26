// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaythosAircraft.Areas.Identity.Data;
using RaythosAircraft.Data;
using RaythosAircraft.Extensions;
using RaythosAircraft.Models;

namespace RaythosAircraft.Controllers
{
    public class PersonalDataModel : Controller
    {
        private readonly RaythosAircraft_db_Context _context;

        public PersonalDataModel(RaythosAircraft_db_Context context)
        {
            _context = context;
        }

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

            return View();
        }
    }
}