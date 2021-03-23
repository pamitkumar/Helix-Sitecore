
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;

    public class UserTestimonial : CustomItem
    {
        public UserTestimonial(Item innerItem) : base(innerItem) { }

        public string NameID
        {
            get
            {
                return References.Templates.UserTestimonial.Fields.Name.ToString();
            }
        }

        public string DesignationID
        {
            get
            {
                return References.Templates.UserTestimonial.Fields.Designation.ToString();
            }
        }

        public string ImageID
        {
            get
            {
                return References.Templates.UserTestimonial.Fields.Image.ToString();
            }
        }

        public string TestimonialID
        {
            get
            {
                return References.Templates.UserTestimonial.Fields.Testimonial.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.UserTestimonial.Fields.IsActive].Value == "1";
            }
        }
    }

}
