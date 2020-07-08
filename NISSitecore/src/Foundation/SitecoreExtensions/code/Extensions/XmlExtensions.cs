using Sitecore.Data;
using Sitecore.StringExtensions;
using System.Xml;
using System.Xml.Linq;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class XmlExtensions
    {
        public static XmlDocument ToXmlDocument(this XNode xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument()
            {
                XmlResolver = (XmlResolver)null
            };
            using (XmlReader reader = xDocument.CreateReader())
                xmlDocument.Load(reader);
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (XmlNodeReader xmlNodeReader = new XmlNodeReader((XmlNode)xmlDocument))
            {
                int content = (int)xmlNodeReader.MoveToContent();
                return XDocument.Load((XmlReader)xmlNodeReader);
            }
        }

        public static XElement ToXElement(this XmlNode node)
        {
            XDocument xdocument = new XDocument();
            using (XmlWriter writer = xdocument.CreateWriter())
                node.WriteTo(writer);
            return xdocument.Root;
        }

        public static ID GetAttributeAsID(this XmlNode node, string key)
        {
            string attributeValue = node.GetAttributeValue(key);
            return attributeValue.IsNullOrEmpty() ? (ID)null : new ID(attributeValue);
        }

        public static string GetAttributeValue(this XmlNode node, string key)
        {
            return node.Attributes?[key]?.Value;
        }
    }
}