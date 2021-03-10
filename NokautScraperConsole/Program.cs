using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {

            /*var lists = new List<ListSite>
            {
                new ListSite { Image="", Link="", Name="Iphone 11", Price = "" }
            };

            using (ListContext context = new ListContext())
            {
                context.ListSites.AddRange(lists);
                context.SaveChanges();
            }*/


             Console.WriteLine("Co chcesz znaleźć taniej ?");
             string typedThing = Console.ReadLine();
             Console.WriteLine("Z jakiej kategorii to jest ?");
             /*
             Typy kategorii typedCategory
             biznes
             delikatesy
             dla dzieci
             dom i ogrod
             erotyka
             fotografia i optyka
             gry i konsole
             inne
             komputery
             kosmetyki
             ksiazki
             moda i styl
             motoryzacja
             muzyka
             narzedzia
             prezenty
             sport i hobby
             sprzet agd
             sprzet rtv
             telefony i akcesoria
             zdrowie
              */
            string typedCategory = Console.ReadLine();

             //Console.WriteLine("Jak posortować ci wyszukiwanie ?" + "\n" + "Nt - Najtańsze " + "\n" + " Nd - Najdrożej");

             var listSites = ScrapList($"http://www.nokaut.pl/{typedCategory}/produkt:{typedThing}");

            //Console.WriteLine("Wyniki wyszukiwania dla : " + typedThing + "W kategorii : " + typedCategory + "\n");
            //coś
            foreach (var item in listSites)
             {
                var products = new List<ListSite>
            {
                new ListSite { Image=item.Image, Link=item.Link, Name=item.Name, Price =item.Price }
            };

                using (ListContext context = new ListContext())
                {
                    context.ListSites.AddRange(products);
                    context.SaveChanges();
                }

                //Console.WriteLine("{0} \n {1} \n {2} \n {3} \n ", item.Name , item.Price ,item.Link ,item.Image);
            }

           
            //Console.WriteLine($"{listSites.Count()} elementów");
            Console.WriteLine("zrobione");    
            Console.ReadKey();
        }

        public static List<ListSite> ScrapList(string html)
        {
            HtmlWeb web = new HtmlWeb();
            List<ListSite> listSites = new List<ListSite>();
            var htmlDoc = web.Load(html);
            try
            {
                foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//div/div")
                .Where(x => x.GetAttributeValue("class", string.Empty)
                .Equals("product-box"))
                .ToList())
                {
                    ListSite element = new ListSite();
                    element.Link = link.SelectSingleNode("a").GetAttributeValue("href", string.Empty);
                    element.Image = link.SelectSingleNode("a/div/img").GetAttributeValue("src", string.Empty);
                    element.Name = link.SelectSingleNode("a/div/img").GetAttributeValue("alt", string.Empty);
                    element.Price = link.SelectSingleNode("a/div/div/div/meta").GetAttributeValue("content", string.Empty);
 
                     listSites.Add(element);                
                }
            }
            catch (Exception)
            {
                
            }
            return listSites;
        }

    }
}
        
      
       
    

