namespace KOProject
{
    public class ListSite
    {
        public int ListSiteID { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }

        public override string ToString()
        {
            return $"{Name}, Price: {Price} ";
        }
    }
}