using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? BrandId { get; set; }

    public int? StockQuantity { get; set; }

    public string? AvatarImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CategoryId { get; set; }
        
    public string ProductStatus { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();

    public virtual ICollection<Color> Colors { get; } = new List<Color>();
}
