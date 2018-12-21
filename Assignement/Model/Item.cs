using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement
{
    internal class Item
    {
        internal Item(int? id, string name, double? price, double? discount)
        {
            Id = id ?? -1;
            Name = name ?? "";
            Price = price ?? 0;
            Discount = discount ?? 0;
        }

        internal int Id { get; set; }
        internal string Name { get; set; }
        internal double Price { get; set; }
        internal double Discount { get; set; }

    }
}
