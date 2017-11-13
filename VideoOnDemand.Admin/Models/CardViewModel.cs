using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemand.Admin.Models
{
    public class CardViewModel
    {
        public int Count { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string BackgroundColor { get; set; }
    }
}
