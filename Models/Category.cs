using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? CategoryImage { get; set; }  
    public virtual ICollection<Category> InverseParentCategory { get; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
    public virtual ICollection<Brand> Brands { get;} = new List<Brand>();
}
