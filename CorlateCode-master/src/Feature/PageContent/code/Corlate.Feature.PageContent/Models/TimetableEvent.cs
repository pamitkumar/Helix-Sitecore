
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    
    public class TimetableEvent : CustomItem
    {
        public TimetableEvent(Item innerItem) : base(innerItem) { }

        public string CourseTitleID
        {
            get
            {
                return References.Templates.TimetableEvent.Fields.CourseTitle.ToString();
            }
        }

        public Instructor Instructor { get; set; }

        public string SelectedInstructorItemID
        {
            get
            {
                return InnerItem.Fields[References.Templates.TimetableEvent.Fields.Instructor].Value;
            }
        }

        public string TimingID
        {
            get
            {
                return References.Templates.TimetableEvent.Fields.Timing.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.TimetableEvent.Fields.IsActive].Value == "1";
            }
        }
    }

}
