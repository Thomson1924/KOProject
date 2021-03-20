using HtmlAgilityPack;
using KOProject.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KOProject
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
       
        public ICommand SearchCommand { get; set; }
        public ICommand GoToStore { get; set; }
        private ObservableCollection<ListSite> products;

        public event PropertyChangedEventHandler PropertyChanged;
        private string keyWord;

        public string KeyWord
        {
            get { return keyWord; }
            set
            {
                keyWord = value;
                PropertyChange(nameof(KeyWord));
            }
        }
        private string category;

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                PropertyChange(nameof(Category));
            }
        }



        public ObservableCollection<ListSite> Products
        {
            get { return products; }
            set
            {
                products = value;
                PropertyChange(nameof(Products));
            }
        }

        public MainWindowViewModel()
        {
            SearchCommand = new SearchProductCommand(this);
            GoToStore = new GoToStoreCommand(this);
        }
        public void ScrapList(string html)
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
                    element.Image = link.SelectSingleNode("a/div/img").GetAttributeValue("src", string.Empty).Insert(0, "http:");
                    element.Name = link.SelectSingleNode("a/div/img").GetAttributeValue("alt", string.Empty);
                    element.Price = link.SelectSingleNode("a/div/div/div/meta").GetAttributeValue("content", string.Empty);

                    listSites.Add(element);
                }
                Products = new ObservableCollection<ListSite>(listSites);
            }
            catch (Exception)
            {

            }


        }

        public void SaveToDataBase(ObservableCollection<ListSite> listSites)
        {
            foreach (var item in listSites)
            {
                var products = new List<ListSite>
                {
                    new ListSite { Image=item.Image, Link=item.Link, Name=item.Name, Price =item.Price }
                };

                using (ListSiteContext context = new ListSiteContext())
                {
                    context.ListSites.AddRange(products);
                    context.SaveChanges();
                }
            }
        }

        private void PropertyChange(string arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }

        public void Store(string link)
        {
            Process.Start(link);
        }
    }
}
