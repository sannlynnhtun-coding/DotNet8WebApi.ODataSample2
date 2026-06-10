using ODataSample.Controllers;
using ODataSample.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ODataSample.Tests
{
    public class CompaniesControllerTests
    {
        private AppDbContext _context = null!;
        private CompaniesController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"CompaniesControllerTests-{Guid.NewGuid()}")
                .Options;

            _context = new AppDbContext(options);
            _context.Companies.AddRange(
                new Company { Id = 1, Name = "Company1", City = "New York" },
                new Company { Id = 2, Name = "Company2", City = "San Francisco" }
            );
            _context.SaveChanges();

            _controller = new CompaniesController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Get_ReturnsAllCompanies()
        {
            var result = _controller.Get() as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            var companies = result!.Value as IEnumerable<Company>;
            Assert.That(companies, Is.Not.Null);
            Assert.That(companies!.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Get_WithValidId_ReturnsCompany()
        {
            var result = _controller.Get(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            var company = result!.Value as Company;
            Assert.That(company, Is.Not.Null);
            Assert.That(company!.Id, Is.EqualTo(1));
        }

        [Test]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            var result = _controller.Get(99);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
