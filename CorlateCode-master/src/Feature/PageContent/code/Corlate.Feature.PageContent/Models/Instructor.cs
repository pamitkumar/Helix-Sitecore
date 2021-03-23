
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;

    public class Instructor : CustomItem
    {
        public Instructor(Item innerItem) : base(innerItem) { }

        public string ImageID
        {
            get
            {
                return References.Templates.Instructor.Fields.Image.ToString();
            }
        }

        public string InstructorNameID
        {
            get
            {
                return References.Templates.Instructor.Fields.InstructorName.ToString();
            }
        }

        public string InstructorName
        {
            get
            {
                return InnerItem.Fields[References.Templates.Instructor.Fields.InstructorName].Value;
            }
        }

        public string DesignationID
        {
            get
            {
                return References.Templates.Instructor.Fields.Designation.ToString();
            }
        }

        public string Designation
        {
            get
            {
                return InnerItem.Fields[References.Templates.Instructor.Fields.Designation].Value;
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.Instructor.Fields.IsActive].Value == "1";
            }
        }
    }

}
