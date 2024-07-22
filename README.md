# DotNet8WebApi.ODataSample2

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

```

dotnet ef dbcontext scaffold "Server=.;Database=ODataSample2;User ID=sa;Password=sasa@123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models -c AppDbContext -f

```