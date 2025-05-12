using Agri_Energy_Connect_Platform.Data;
using Agri_Energy_Connect_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agri_Energy_Connect_Platform.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Product(string search, int? farmerId, string category, DateTime? startDate, DateTime? endDate)
        {
            var farmers = await _context.Farmers.ToListAsync();
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                products = products.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => p.Category.Contains(category));

            if (farmerId.HasValue)
                products = products.Where(p => p.FarmerId == farmerId.ToString());

            if (startDate.HasValue)
                products = products.Where(p => p.dateTime >= startDate.Value);

            if (endDate.HasValue)
                products = products.Where(p => p.dateTime <= endDate.Value);

            ViewBag.Farmers = farmers;
            ViewBag.Search = search;
            ViewBag.SelectedFarmerId = farmerId;
            ViewBag.Category = category;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(await products.ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }
    }
}
