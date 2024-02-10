using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? Description { get; set; }

    public string? Country { get; set; }
    public int CategoryId { get; set; } // Thêm trường CategoryId
    public virtual Category? Category { get; set; }
    
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
