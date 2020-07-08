using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Collections.Specialized;

namespace NISSitecore.Foundation.Sitemap.Models
{
    
    [Serializable]
    [XmlRoot("urlset")]
    public class Urlset
    {
        ///

        /// Constructor to initialize Url Object
        /// 

        public Urlset() {
            //Url = new object[];
        }       

        
        ///

        /// Urls collection
        /// 

        //[XmlElement("url")]
        //public List<object> Url { get; set; }

        [XmlElement("url")]
        public object[] Url { get; set; }
    }

    public class Url
    {
        ///

        /// Location Parameter
        /// 

        [XmlElement("loc")]
        public string Loc { get; set; }

        ///

        /// Last modified on
        /// 

        [XmlElement("lastmod")]
        public string Lastmod { get; set; }

        //Add required properties here like changefreq, priority
    }
}