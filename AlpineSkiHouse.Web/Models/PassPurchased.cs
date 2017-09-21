﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models
{
    public class PassPurchased
    {
        public int PassTypeId { get; set; }

        public int CardId { get; set; }

        public decimal PricePaid { get; set; }

        public string DiscountCode { get; set; }
    }
}
