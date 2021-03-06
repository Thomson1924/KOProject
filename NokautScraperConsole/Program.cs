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
            //string typedThing = "Iphone";
            var listSites = /*ScrapListFromPage*/ @"http://www.nokaut.pl/produkt:iphone";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(listSites);

            var name = htmlDoc.DocumentNode.SelectSingleNode("html/body/section/div/div/section/div/div/a/div/div/h2");// Wyciaganie nazwy single 
            var price = htmlDoc.DocumentNode.SelectSingleNode("html/body/section/div/div/section/div/div/a/div/div/div/strong/text"); // Wyciaanie ceny single
            //var img = htmlDoc.DocumentNode.SelectSingleNode("html/body/section/div/div/a/div/img"); Dodać jeszcze source.

            Console.WriteLine(name.InnerHtml + "/n" + price.InnerHtml + "/n" + /* img.InnerHtml + */  "/n" + );
            
           /* foreach (var item in listSites)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", item.Name, item.Link, item.Image, item.Price, item.Rate);
            }
            Console.WriteLine($"{listSites.Count()} elementów"); */
            Console.ReadKey();
        }
        
        /*public static List<ListSite> ScrapListFromPage(string html)
        {
            HtmlWeb web = new HtmlWeb();
            List<ListSite> listSites = new List<ListSite>();
            var htmlDoc = web.Load(html);
            try
            {
                foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//div/a/")
                    .Where(x => x.GetAttributeValue("class", string.Empty)
                    .Equals("product-box"))
                    .ToList())
                {

                    ListSite element = new ListSite();
                    element.Link = link.SelectSingleNode("div/div/section/div/div/a")
                                        .GetAttributeValue("href", string.Empty);
                    element.Name = link.SelectSingleNode("div/div/section/div/div/a/div/div/h2")
                                        .InnerText;

                   if (link.SelectSingleNode("div/div/section/div/div/a/div/img")
                            .GetAttributeValue("src", string.Empty)
                            .Equals("/content/img/icons/pix-empty.png"))
                    {
                        element.Image = link.SelectSingleNode("")
                                            .GetAttributeValue("data-original", string.Empty);
                    }
                    else
                    {
                        element.Image = link.SelectSingleNode("div/a/img")
                                            .GetAttributeValue("src", string.Empty);
                    }

                    element.Price = link.SelectNodes("div/div")
                                        .Where(x => x.GetAttributeValue("class", string.Empty)
                                        .Equals("cat-prod-row__price"))
                                        .First()
                                        .SelectSingleNode("a//span/span")
                                        .InnerText;

                    listSites.Add(element);
                }
            }
            catch (Exception)
            {
            }
            
            return listSites;
        }*/
       
    }
}
