using System;
using System.Collections.Generic;

namespace MobileStore.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? CouponCode { get; set; }

    public decimal? DiscountAmount { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
