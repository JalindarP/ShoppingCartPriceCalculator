using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement.Model
{
    public class Offer
    {
        public Offer(Dictionary<int, int> buy, Dictionary<int, int> free)
        {
            Buy = buy;
            Free = free;
        }

        public Dictionary<int, int> Buy { get; set; }
        public Dictionary<int, int> Free { get; set; }
    }
}
