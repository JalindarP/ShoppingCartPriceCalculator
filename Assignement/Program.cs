using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = ReadShoppingItems();

            List<Offer> offers = ReadOffers();

            DisplayItems(items);
            DisplayOffers(items, offers);
            var cart = AddInCart(items);

            Console.WriteLine("");
            Console.WriteLine("S/N       Name        Quantity       Price");
            DisplayCart(cart);

            Console.WriteLine();
            Console.WriteLine("Your Free Items***");
            var freeItems = AddInFreeCart(items, cart.ToList(), offers);
            DisplayCart(freeItems);
            Console.WriteLine();

            DisplayPrice(cart);

            Console.Read();
        }

        private static List<Offer> ReadOffers()
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

        private static List<Item> ReadShoppingItems()
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

        private static IEnumerable<Item> AddInFreeCart(List<Item> items, List<Item> cart, List<Offer> offers)
        {
            List<Item> freeCart = new List<Item>();
           
            foreach(var offer in offers)
            {
                bool isOffermatched = true;
                foreach (var buy in offer.Buy)
                {
                    if (cart.Count(x => x.Id ==  buy.Key) <  buy.Value)
                    {
                        isOffermatched = false;
                    }
                }
                if(isOffermatched)
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

        private static void DisplayPrice(IEnumerable<Item> items)
        {
            double price = 0;
            foreach(var item in items)
            {
                price += (item.Price - ((item.Discount/100)*item.Price));
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Total Amount = "+price);
            Console.WriteLine();
            Console.WriteLine("NO GST, Enjoy!!");
        }

        private static IEnumerable<Item> AddInCart(List<Item> items)
        {
            List<Item> cart = new List<Item>();
            Console.WriteLine("");
            Console.WriteLine("Add items in your cart. Enter Item Id to add in your cart. Press 'E' any time to exit.");
            bool flag = true;
            while(flag)
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
        
        private static void DisplayOffers(List<Item> items, List<Offer> offers)
        {
            Console.WriteLine("**********Offers**********");
            Console.WriteLine("");
            foreach (var offer in offers)
            {
                foreach (var item in offer.Buy)
                {
                    Console.Write(" Buy " + item.Value +" " + (items.First(x => x.Id == item.Key)).Name + ", ");
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
    }
}
