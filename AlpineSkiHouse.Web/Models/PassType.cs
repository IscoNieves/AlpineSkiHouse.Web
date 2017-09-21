using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models
{
    public class PassType
    {
        public PassType()
        {
            PassTypeResorts = new List<PassTypeResort>();
            Prices = new List<PassTypePrice>();
        }

        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public int MaxActivations { get; set; }

        public List<PassTypeResort> PassTypeResorts { get; set; }

        public List<PassTypePrice> Prices { get; set; }
    }
}
