using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models.SkiCardViewModels
{
    public class CreateSkiCardViewModel
    {
        public string CardHolderPhoneNumber { get; set; }

        public string CardHolderFirstName { get; set; }

        public string CardHolderLastName { get; set; }

        public DateTime? CardHolderBirthDate { get; set; }
    }
}
