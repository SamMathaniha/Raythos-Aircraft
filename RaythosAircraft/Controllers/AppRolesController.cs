using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

//A controller for providing option user user to add new roles
namespace RaythosAircraft.Controllers
{
    public class AppRolesController : Controller
    {
        //Creating a variable
        private readonly RoleManager<IdentityRole> _roleManager;

        //Creating a constructor
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //List all the roles created by users
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        //A Method to return a view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //A Method to create a new Role
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //Avoid duplicate role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
