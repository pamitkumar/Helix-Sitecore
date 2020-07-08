using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links.UrlBuilders;
using Sitecore.Resources.Media;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class MediaItemExtensions
    {
        public static string GetMediaUrl(this MediaItem mediaItem, MediaUrlBuilderOptions options = null)
        {
            if (options == null)
                options = new MediaUrlBuilderOptions();
            string mediaUrl = MediaManager.GetMediaUrl(mediaItem, options);
            return mediaUrl.Contains("://") ? mediaUrl : StringUtil.EnsurePrefix('/', mediaUrl);
        }

        public static bool IsImage(this Sitecore.Resources.Media.Media mediaItem)
        {
            return mediaItem != null && mediaItem.MimeType.Contains("image/");
        }
    }
}