using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaythosAircraft.Models;
using RaythosAircraft.Areas.Identity.Data;

namespace RaythosAircraft.Data;

public class RaythosAircraft_db_Context : IdentityDbContext<User>
{
    public RaythosAircraft_db_Context(DbContextOptions<RaythosAircraft_db_Context> options)
        : base(options)
    {
    }

    //DbSet properties for database tables
    public DbSet<User> User { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Payment> Payment { get; set; }

    //public DbSet<RaythosAircraft.Models.ProductDetailViewModel>? ProductDetailViewModel { get; set; }

}
