using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Common.Website.App_Start
{
    using System.Collections.Generic;
    using Sitecore.Configuration;
    using System.Xml;
    using Sitecore.Xml;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResourceConfig
    {
        //The two properties corresponding to the properties into the config
        public string Name { get; set; }
        public string MyProperty { get; set; }

        //The method who return the list of my custom elements
        public static List<ResourceConfig> GetListJavaScript()
        {
            List<ResourceConfig> lst = new List<ResourceConfig>();

            //Read the configuration nodes
            foreach (XmlNode node in Factory.GetConfigNodes("Javascript/ResourceItem"))
            {
                //Create a element of this type
                ResourceConfig elem = new ResourceConfig();
                elem.Name = XmlUtil.GetAttribute("Name", node);
                elem.MyProperty = XmlUtil.GetAttribute("MyProperty", node);
                lst.Add(elem);
            }
            return lst;
        }

        public static List<ResourceConfig> GetListStyles()
        {
            List<ResourceConfig> lst = new List<ResourceConfig>();

            //Read the configuration nodes
            foreach (XmlNode node in Factory.GetConfigNodes("Styles/ResourceItem"))
            {
                //Create a element of this type
                ResourceConfig elem = new ResourceConfig();
                elem.Name = XmlUtil.GetAttribute("Name", node);
                elem.MyProperty = XmlUtil.GetAttribute("MyProperty", node);
                lst.Add(elem);
            }

            return lst;
        }
    }
}