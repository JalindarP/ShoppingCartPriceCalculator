using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignement.CartBL.Interface;
using Assignement.Model;

namespace Assignement.CartBL
{
    public sealed class PriceCalculator: IPriceCalculator, IDisposable
    {

        List<Offer> IPriceCalculator.ReadOffers()
        {
            List<Offer> offers = new List<Offer>();
            using (StreamReader stream = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Input\Offer.csv")))
            {
                stream.ReadLine();
                while (stream.Peek() >= 0)
                {
                    string[] offer = stream.ReadLine().Split('=');
                    string[] buy = offer[0].Split('+');
                    string[] free = offer[1].Split('+');

                    offers.Add(new Offer(BuildKeyValue(buy.ToList()), BuildKeyValue(free.ToList())));
                }
            }

            return offers;
        }

        List<Item> IPriceCalculator.ReadShoppingItems()
        {
            List<Item> items = new List<Item>();
            using (StreamReader stream = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Input\Items.csv")))
            {
                stream.ReadLine();
                while (stream.Peek() >= 0)
                {
                    string[] item = stream.ReadLine().Split(',');
                    items.Add(new Item(Convert.ToInt32(item[0]), Convert.ToString(item[1]), Convert.ToDouble(item[2]), Convert.ToDouble(item[3])));
                }
            }

            return items;
        }

        IEnumerable<Item> IPriceCalculator.AddFreeItems(List<Item> items, List<Item> cart, List<Offer> offers)
        {
            List<Item> freeCart = new List<Item>();

            foreach (var offer in offers)
            {
                bool isOffermatched = true;
                foreach (var buy in offer.Buy)
                {
                    if (cart.Count(x => x.Id == buy.Key) < buy.Value)
                    {
                        isOffermatched = false;
                    }
                }
                if (isOffermatched)
                {
                    foreach (var free in offer.Free)
                    {
                        freeCart.Add(new Item(free.Key, items.First(x => x.Id == free.Key).Name, 0, 0));
                    }

                    foreach (var buy in offer.Buy)
                    {
                        int iterator = buy.Value;
                        while (iterator > 0)
                        {
                            var item = cart.First(x => x.Id == buy.Key);
                            cart.Remove(item);
                            iterator--;
                        }
                    }

                }
            }

            return freeCart;
        }

        IEnumerable<Item> IPriceCalculator.AddInCart(List<Item> items)
        {
            List<Item> cart = new List<Item>();           
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("");
                Console.WriteLine("Add item in your cart");
                string s = Console.ReadLine();
                if (s.ToLower().Equals("e"))
                    flag = false;
                else if (Int32.TryParse(s, out int index))
                {
                    cart.Add(items.First(x => x.Id == index));
                }
            }

            return cart;
        }
   
        private static Dictionary<int, int> BuildKeyValue(List<string> items)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            while (items.Count > 0)
            {
                result[Convert.ToInt32(items.First())] = items.Count(x => x == items.First());
                items.RemoveAll(x => x == items.First());
            }
            return result;
        }

        public void Dispose()
        {
           
        }
    }
}
