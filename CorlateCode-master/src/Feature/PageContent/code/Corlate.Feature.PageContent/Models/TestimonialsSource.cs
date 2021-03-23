
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class TestimonialsSource : CustomItem
    {
        public TestimonialsSource(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.TestimonialsSource.Fields.Title.ToString();
            }
        }

        public List<UserTestimonial> Testimonials { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.TestimonialsSource.Fields.IsActive].Value == "1";
            }
        }
    }

}
