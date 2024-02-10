using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();

    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();
}
