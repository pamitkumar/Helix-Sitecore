
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore;

    public class Constants
    {
        public static int MaxCourseTeasersPerPage = MainUtil.GetInt(Sitecore.Configuration.Settings.GetSetting("MaxCourseTeasersPerPage"), 1);
        public static int MaxBlogTeasersPerPage = MainUtil.GetInt(Sitecore.Configuration.Settings.GetSetting("MaxBlogTeasersPerPage"), 1);
    }
}