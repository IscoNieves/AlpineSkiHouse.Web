using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models
{
    public class SkiCard
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(70)]
        [Required]
        public string CardHolderFirstName { get; set; }

        [MaxLength(70)]
        public string CardHolderLastName { get; set; }

        public DateTime CardHolderBirthDate { get; set; }

        [Phone]
        public string CardHolderPhoneNumber { get; set; }
    }
}
