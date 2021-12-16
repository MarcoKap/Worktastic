using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

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

        [HttpGet]
        public IActionResult GetJob(int id)
        {
            if (id == 0)
                return BadRequest();

            var jobFromDb = _context.Jobs.Single((j) => j.Id == id);

            if (jobFromDb == null)
                return NotFound();

            return Ok(jobFromDb);
        }

        public IActionResult GetJobPartial(string query)
        {
            List<JobModel> jobs = new List<JobModel>();

            if (string.IsNullOrWhiteSpace(query))
            {
                jobs = _context.Jobs.ToList();
            }
            else
            {
                jobs = _context.Jobs.Where((j) => j.JobTitle.ToLower().Contains(query.ToLower())).ToList();
            }

            return PartialView("_JobListPartial", jobs);
        }
    }
}
