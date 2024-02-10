using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class Color
{
    public int ColorId { get; set; }

    public string? ColorName { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
