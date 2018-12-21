using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignement.Model;

namespace Assignement.CartBL.Interface
{
    public interface IPriceCalculator
    {
        List<Offer> ReadOffers();

        List<Item> ReadShoppingItems();

        IEnumerable<Item> AddFreeItems(List<Item> items, List<Item> cart, List<Offer> offers);

        IEnumerable<Item> AddInCart(List<Item> items);
    }
}
