using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement
{
    internal class Offer
    {
        internal Offer(Dictionary<int, int> buy, Dictionary<int, int> free)
        {
            Buy = buy;
            Free = free;
        }

        internal Dictionary<int, int> Buy { get; set; }
        internal Dictionary<int, int> Free { get; set; }
    }

}
