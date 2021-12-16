using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Worktastic.Data;
using Worktastic.Filters;
using Worktastic.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Worktastic.Controllers
{
    [Route("api/job")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class ApiJobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiJobController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet("all")]
        public IEnumerable<JobModel> Get()
        {
            var jobs = _context.Jobs.ToList();
            return jobs;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JobModel Get(int id)
        {
            var jobFromDb = _context.Jobs.First(j => j.Id == id);
            return jobFromDb;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
