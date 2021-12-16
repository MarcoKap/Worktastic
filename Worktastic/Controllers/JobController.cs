using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worktastic.Data;
using Worktastic.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Worktastic.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var allJobs = _context.Jobs.ToList();
                return View(allJobs);
            }

            var jobs = _context.Jobs.Where(j => j.OwnerUsername == User.Identity.Name).ToList();
            return View(jobs);
        }

        public IActionResult CreateEditJob(int id) {
            if (id != 0)
            {
                var jobFromDb = _context.Jobs.FirstOrDefault(j => j.Id == id);

                if ((jobFromDb.OwnerUsername != User.Identity.Name) && !User.IsInRole("Admin"))
                    return Unauthorized();

                if (jobFromDb != null)
                {
                    return View(jobFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteJobPosting(int id)
        {
            if (id == 0)
                return BadRequest();

            var jobPostingFromDb = _context.Jobs.Single((job) => job.Id == id);

            if (jobPostingFromDb == null)
                return NotFound();

            _context.Jobs.Remove(jobPostingFromDb);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult CreateEditJobEntry(JobModel job, IFormFile file)
        {
            job.OwnerUsername = User.Identity.Name;

            if(file != null)
            {
                using(var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    job.CompanyImage = bytes;
                }
            }

            if (job.Id == 0)
            {
                // Neuen Job hinzufügen
                _context.Jobs.Add(job);
            } else
            {
                var jobInDb = _context.Jobs.SingleOrDefault(j => j.Id == job.Id);
                if (jobInDb == null)
                {
                    return NotFound();
                }
                jobInDb.CompanyImage = job.CompanyImage;
                jobInDb.CompanyName = job.CompanyName;
                jobInDb.ContactMail = job.ContactMail;
                jobInDb.ContactName = job.ContactName;
                jobInDb.ContactPhone = job.ContactPhone;
                jobInDb.ContactWebsite = job.ContactWebsite;
                jobInDb.Description = job.Description;
                jobInDb.JobLocation = job.JobLocation;
                jobInDb.JobTitle = job.JobTitle;
                jobInDb.Salary = job.Salary;
                jobInDb.StartDate = job.StartDate;


            }
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
