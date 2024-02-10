
using System;
using System.Collections.Generic;
namespace MobileStore.Models
{
    public class Banner
    {
        public int BannerId { get; set; }
        public string BannerImage { get; set; }

        public string BannerDetail { get; set;}
        public DateTime? BannerDateAdded { get; set; }  
        public string BannerStatus { get; set; }
    }
}
