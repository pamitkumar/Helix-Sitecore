
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class FacetsSource : CustomItem
    {
        public FacetsSource(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.FacetsSource.Fields.Title.ToString();
            }
        }

        public List<Facet> Facets { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.FacetsSource.Fields.IsActive].Value == "1";
            }
        }
    }

}
