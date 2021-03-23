
namespace Corlate.Feature.PageContent.Models
{
    using Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class CourseDetails : CustomItem
    {
        public CourseDetails(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.Title.ToString();
            }
        }

        public string TeaserImageID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.TeaserImage.ToString();
            }
        }

        public string MainImageID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.MainImage.ToString();
            }
        }

        public Instructor Instructor { get; set; }

        public string SelectedInstructorItemID
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseDetails.Fields.Instructor].Value;
            }
        }


        public string CostID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.Cost.ToString();
            }
        }

        public string DurationID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.Duration.ToString();
            }
        }

        public string ContentHeadingID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.ContentHeading.ToString();
            }
        }
        
        public string BriefDescriptionID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.BriefDescription.ToString();
            }
        }

        public string ContentID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.Content.ToString();
            }
        }

        public string ViewCourseLabelID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.ViewCourseLabel.ToString();
            }
        }

        public string TimetableColumn1LabelID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.TimetableColumn1Label.ToString();
            }
        }

        public string TimetableColumn1Label
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseDetails.Fields.TimetableColumn1Label].Value;
            }
        }


        public string TimetableColumn2LabelID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.TimetableColumn2Label.ToString();
            }
        }

        public string TimetableColumn2Label
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseDetails.Fields.TimetableColumn2Label].Value;
            }
        }

        public string TimetableColumn3LabelID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.TimetableColumn3Label.ToString();
            }
        }

        public string TimetableColumn3Label
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseDetails.Fields.TimetableColumn3Label].Value;
            }
        }

        public List<TimetableEvent> TimetableEvents { get; set; }

        public string MoreCoursesSectionLabelID
        {
            get
            {
                return References.Templates.CourseDetails.Fields.MoreCoursesSectionLabel.ToString();
            }
        }

        public List<CourseDetails> MoreCourses { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseDetails.Fields.IsActive].Value == "1";
            }
        }

        public string PageURL
        {
            get
            {
                return SitecoreUtility.GetItemURL(InnerItem);
            }
        }
    }

}
