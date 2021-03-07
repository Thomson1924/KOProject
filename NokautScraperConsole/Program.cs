﻿using HtmlAgilityPack;
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

            // Console.WriteLine("Jak posortować ci wyszukiwanie ?" + "\n" + "Nt - Najtańsze " + "\n" + " Nd - Najdrożej");
            
            var listSites = ScrapList($"http://www.nokaut.pl/{typedCategory}/produkt:{typedThing}");

           Console.WriteLine("Wyniki wyszukiwania dla : " + typedThing + "W kategorii : " + typedCategory + "\n");


            foreach (var item in listSites)
            {
                Console.WriteLine("{0} \n {1} \n {2} \n {3} \n ", item.Name , item.Price ,item.Link ,item.Image);
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
        public static ScrappedData ScrapWithDetails(string html)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode($"//script");
            try
            {
                var elementDetails = JsonConvert.DeserializeObject<ScrappedData>(node.InnerText);
                return elementDetails;
            }
            catch (Exception)
            {
                return new ScrappedData();
            }
        }
    }
        
      
       
    }

