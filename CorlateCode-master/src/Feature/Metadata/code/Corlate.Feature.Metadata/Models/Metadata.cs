namespace Corlate.Feature.Metadata.Models
{
    using Sitecore.Data.Items;

    public class Metadata : CustomItem
    {
        
        public Metadata(Item innerItem) : base(innerItem)
        {
        }

        /// <summary>
        /// Gets the Title
        /// </summary>
        public string Title
        {
            get
            {
                return InnerItem.Fields[References.Templates.Metadata.Fields.Title].Value;
            }
        }

        /// <summary>
        /// Gets the Description
        /// </summary>
        public string Description
        {
            get
            {
                return InnerItem.Fields[References.Templates.Metadata.Fields.Description].Value;
            }
        }

        /// <summary>
        /// Gets the Keywords
        /// </summary>
        public string Keywords
        {
            get
            {
                return InnerItem.Fields[References.Templates.Metadata.Fields.Keywords].Value;
            }
        }
    }
}
