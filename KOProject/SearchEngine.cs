using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack

namespace KOProject
{
    public class SearchEngine
    {
        

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
