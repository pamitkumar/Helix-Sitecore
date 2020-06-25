using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.Configuration
{
    public class MappingComponent
    {
        public Dictionary<Guid, string> ItemNames { get; private set; }       

        public MappingComponent()
        {
            this.ItemNames = new Dictionary<Guid, string>();
        }

        public void AddItemName(string key, System.Xml.XmlNode node)
        {
            AddItemName(node);
        }
        public void AddItemName(System.Xml.XmlNode node)
        {
            var guid = Sitecore.Xml.XmlUtil.GetAttribute("guid", node);
            var name = Sitecore.Xml.XmlUtil.GetChildValue("name", node);
            this.ItemNames.Add(new Guid(guid), name);
        }
    }

    //public class ConfigMapping
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //}


}