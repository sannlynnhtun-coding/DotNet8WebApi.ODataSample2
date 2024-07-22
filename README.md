# DotNet8WebApi.ODataSample2

This project is a sample implementation of a .NET 8 Web API with OData integration, demonstrating how to build and manage data using Entity Framework Core and SQL Server.

## Features

- **OData Integration**: Enables querying, filtering, and managing data with OData.
- **Entity Framework Core**: Uses EF Core for data access and management.
- **SQL Server**: Sample database setup with two tables: Companies and Products.
- **Scaffolding**: Demonstrates scaffolding the database context using EF Core.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio or any C# IDE

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/sannlynnhtun-coding/DotNet8WebApi.ODataSample2
   cd DotNet8WebApi.ODataSample2
   ```

2. Set up the database:
   - Create a SQL Server database.
   - Update the connection string in `appsettings.json`.

3. Scaffold the database context:
   ```sh
   dotnet ef dbcontext scaffold "Your_Connection_String" Microsoft.EntityFrameworkCore.SqlServer -o Models
   ```

4. Run the application:
   ```sh
   dotnet run
   ```

### Usage

- Access the API at `https://localhost:5001/odata`
- Use OData queries to manage and filter data.


### SQL scripts:

```sql

CREATE TABLE Companies (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    City NVARCHAR(100)
);

CREATE TABLE Products (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    CompanyID INT,
    FOREIGN KEY (CompanyID) REFERENCES Companies(ID)
);


```

### EFCore DB-First command in terminal:

```

dotnet ef dbcontext scaffold "Server=.;Database=ODataSample2;User ID=sa;Password=sasa@123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models -c AppDbContext -f

```
