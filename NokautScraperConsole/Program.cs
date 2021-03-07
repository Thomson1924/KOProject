using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityPack
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.WriteLine("Co chcesz znaleźć taniej ?");
            string typedThing = Console.ReadLine();
           
            var listSites = ScrapList($"http://www.nokaut.pl/produkt:{typedThing}");

            Console.WriteLine("Wyniki wyszukiwania dla : " + typedThing);


            foreach (var item in listSites)
            {
                Console.WriteLine("{0} {1} ", item.Link , item.Name);
            }
            Console.WriteLine($"{listSites.Count()} elementów");
            Console.ReadKey();
        }

        public static List<ListSite> ScrapList(string html)
        {
            HtmlWeb web = new HtmlWeb();
            List<ListSite> listSites = new List<ListSite>();
            var htmlDoc = web.Load(html);
            try
            {
                foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//html/body/section/div/div/section")
                    .Where(x => x.GetAttributeValue("class", string.Empty)
                    .Equals("ProductList"))
                    .ToList())
                {
                    ListSite element = new ListSite();
                    element.Link = link.SelectSingleNode("html/body/section/div/div/section/div/div/a")
                                    .GetAttributeValue("href", string.Empty);
                    element.Name = link.SelectSingleNode("html/body/section/div/div/section/div/div/a/div/div/h2")
                                    .InnerText;

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

