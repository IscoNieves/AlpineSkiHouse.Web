using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models
{
    public class PassAdded : INotification
    {
        public int PassId { get; set; }

        public int PassTypeId { get; set; }

        public int CardId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
