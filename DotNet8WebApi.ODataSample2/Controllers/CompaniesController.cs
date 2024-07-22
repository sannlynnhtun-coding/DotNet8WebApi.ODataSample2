using DotNet8WebApi.ODataSample2.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNet8WebApi.ODataSample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ODataController
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Companies);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == key);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
    }
}
