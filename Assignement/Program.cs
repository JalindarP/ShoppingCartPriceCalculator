using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignement.CartBL;
using Assignement.CartBL.Interface;
using Assignement.Model;

namespace Assignement
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            IPriceCalculator pc = new PriceCalculator();

            List<Item> items = pc.ReadShoppingItems();

            List<Offer> offers = pc.ReadOffers();

            DisplayItems(items);
            DisplayOffers(items, offers);

            Console.WriteLine("");
            Console.WriteLine("Add items in your cart. Enter Item Id to add in your cart. Press 'E' any time to exit.");
            var cart = pc.AddInCart(items);

            Console.WriteLine("");
            Console.WriteLine("S/N       Name        Quantity       Price");
            DisplayCart(cart);

            Console.WriteLine();
            Console.WriteLine("Your Free Items***");
            var freeItems = pc.AddFreeItems(items, cart.ToList(), offers);
            DisplayCart(freeItems);
            Console.WriteLine();

            DisplayPrice(cart);

            Console.Read();
        }

        private static void DisplayPrice(IEnumerable<Item> items)
        {
            double price = 0;
            foreach (var item in items)
            {
                price += (item.Price - ((item.Discount / 100) * item.Price));
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Total Amount = " + price);
            Console.WriteLine();
            Console.WriteLine("NO GST, Enjoy!!");
        }

        private static void DisplayCart(IEnumerable<Item> items)
        {
            var list = items.ToList();
            int iterator = 1;
            while (list.Any())
            {
                Console.WriteLine();
                var item = list.First();
                int count = list.Count(x => x.Id == item.Id);
                double price = (item.Price - ((item.Discount / 100) * item.Price)) * count;
                Console.WriteLine("");
                Console.WriteLine(iterator + ".       " + item.Name + "         " + count + "           " + price);
                list.RemoveAll(x => x.Id == item.Id);
                iterator++;
            }
        }

        private static void DisplayOffers(List<Item> items, List<Offer> offers)
        {
            Console.WriteLine("**********Offers**********");
            Console.WriteLine("");
            foreach (var offer in offers)
            {
                foreach (var item in offer.Buy)
                {
                    Console.Write(" Buy " + item.Value + " " + (items.First(x => x.Id == item.Key)).Name + ", ");
                }
                foreach (var item in offer.Free)
                {
                    Console.Write(" Get " + item.Value + " " + (items.First(x => x.Id == item.Key)).Name + " Free,");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("**********Happy Shopping**********");
        }

        private static void DisplayItems(List<Item> items)
        {
            Console.WriteLine("**********Add in Your Cart**********");
            Console.WriteLine("");
            Console.WriteLine("Id       Name        Price       Discount");
            Console.WriteLine("");
            foreach (var item in items)
            {
                Console.WriteLine(item.Id + "     " + item.Name + "       " + item.Price + "      " + item.Discount);
                Console.WriteLine("");
            }
        }



    }


}
