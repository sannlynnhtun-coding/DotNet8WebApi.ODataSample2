using ODataSample.Database.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers()
    .AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents("api", GetEdmModel()));

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ODataSample"));

var app = builder.Build();

SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Company>("Companies");
    odataBuilder.EntitySet<Product>("Products");
    return odataBuilder.GetEdmModel();
}

static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (context.Companies.Any())
    {
        return;
    }

    context.Companies.AddRange(
        new Company { Id = 1, Name = "Company1", City = "New York" },
        new Company { Id = 2, Name = "Company2", City = "San Francisco" }
    );

    context.Products.AddRange(
        new Product { Id = 1, Name = "Laptop", CompanyId = 1 },
        new Product { Id = 2, Name = "Phone", CompanyId = 1 },
        new Product { Id = 3, Name = "Tablet", CompanyId = 2 }
    );

    context.SaveChanges();
}
