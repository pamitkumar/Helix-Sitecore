
namespace Corlate.Feature.PageContent.Models
{
    using System.Collections.Generic;

    public class CourseArchive
    {
        public List<CourseDetails> CourseTeasers { get; set; }

        public string FirstAndLastCourseItemIDsInPage { get; set; }

        public bool ShowPreviousButton { get; set; }

        public bool ShowNextButton { get; set; }
    }
}