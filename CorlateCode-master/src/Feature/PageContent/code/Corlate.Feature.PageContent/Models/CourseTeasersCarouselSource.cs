
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class CourseTeasersCarouselSource : CustomItem
    {
        public CourseTeasersCarouselSource(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.CourseTeasersCarouselSource.Fields.Title.ToString();
            }
        }

        public string CourseArchivePageID
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseTeasersCarouselSource.Fields.CourseArchivePage].Value;
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.CourseTeasersCarouselSource.Fields.IsActive].Value == "1";
            }
        }

        public List<CourseDetails> CourseTeasers { get; set; }
    }

}
