using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement.Model
{
    public class Item
    {
        public Item(int? id, string name, double? price, double? discount)
        {
            Id = id ?? -1;
            Name = name ?? "";
            Price = price ?? 0;
            Discount = discount ?? 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
