using DotNet8WebApi.ODataSample2.Controllers;
using DotNet8WebApi.ODataSample2.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DotNet8WebApi.ODataSample2.Tests
{
    public class CompaniesControllerTests
    {
        private Mock<AppDbContext> _mockContext;
        private CompaniesController _controller;

        [SetUp]
        public void Setup()
        {
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company1", City = "New York" },
                new Company { Id = 2, Name = "Company2", City = "San Francisco" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Company>>();
            mockSet.As<IQueryable<Company>>().Setup(m => m.Provider).Returns(companies.Provider);
            mockSet.As<IQueryable<Company>>().Setup(m => m.Expression).Returns(companies.Expression);
            mockSet.As<IQueryable<Company>>().Setup(m => m.ElementType).Returns(companies.ElementType);
            mockSet.As<IQueryable<Company>>().Setup(m => m.GetEnumerator()).Returns(companies.GetEnumerator());

            _mockContext = new Mock<AppDbContext>();
            _mockContext.Setup(c => c.Companies).Returns(mockSet.Object);

            _controller = new CompaniesController(_mockContext.Object);
        }

        [Test]
        public void Get_ReturnsAllCompanies()
        {
            var result = _controller.Get() as OkObjectResult;
            Assert.IsNotNull(result);

            var companies = result.Value as IEnumerable<Company>;
            Assert.AreEqual(2, companies.Count());
        }

        [Test]
        public void Get_WithValidId_ReturnsCompany()
        {
            var result = _controller.Get(1) as OkObjectResult;
            Assert.IsNotNull(result);

            var company = result.Value as Company;
            Assert.AreEqual(1, company.Id);
        }

        [Test]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            var result = _controller.Get(99) as NotFoundResult;
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
