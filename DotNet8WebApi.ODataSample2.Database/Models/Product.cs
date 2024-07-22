using System;
using System.Collections.Generic;

namespace DotNet8WebApi.ODataSample2.Database.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
