using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zagrean_Robert_project.Models;
using Microsoft.EntityFrameworkCore;
using Zagrean_Robert_project.Data;
using Zagrean_Robert_project.Models.LibraryViewModels;

namespace Zagrean_Robert_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly SuplimenteContext _context;
        public HomeController(SuplimenteContext context)
        {
            _context = context;
        }
        private readonly ILogger<HomeController> _logger;

        

        public IActionResult Index()
        {
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
        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from comanda in _context.Comenzis
            group comanda by comanda.DataComanda into dateGroup
            select new OrderGroup()
            {
                OrderDate = dateGroup.Key,
                BookCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}
