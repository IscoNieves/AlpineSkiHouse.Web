using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models
{
    public class PassTypePrice
    {
        public int Id { get; set; }

        [Range(0, 120)]
        public int MinAge { get; set; }

        [Range(1, 120)]
        public int MaxAge { get; set; }

        public decimal Price { get; set; }

        public int PassTypeId { get; set; }
    }
}
