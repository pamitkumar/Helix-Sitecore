
namespace Corlate.Foundation.Common.Gutters
{
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Shell.Applications.ContentEditor.Gutters;

    public class PublishedStatusGutter : GutterRenderer
    {
        private enum PublishedStatus
        {
            NeverPublished,
            Published,            
            Modified
        }

        private PublishedStatus CheckPublishedStatus(Item currentItem)
        {
            Database webDb = Factory.GetDatabase("web");
            Item webItem = webDb.GetItem(currentItem.ID);

            if (webItem == null)
            {
                return PublishedStatus.NeverPublished;
            }
            else if (currentItem["__Revision"] != webItem["__Revision"])
            {
                return PublishedStatus.Modified;
            }
            else
            {
                return PublishedStatus.Published;
            }
        }

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            PublishedStatus publishedStatus = CheckPublishedStatus(item);

            if (publishedStatus != PublishedStatus.Published)
            {
                GutterIconDescriptor gutterIconDescriptor = new GutterIconDescriptor();

                if (publishedStatus == PublishedStatus.NeverPublished)
                {
                    gutterIconDescriptor.Icon = "Applications/32x32/bullet_ball_red.png";
                    gutterIconDescriptor.Tooltip = "Item was never published";
                }
                else
                {
                    gutterIconDescriptor.Icon = "Applications/32x32/bullet_ball_yellow.png";
                    gutterIconDescriptor.Tooltip = "Item is published but modified";
                }

                gutterIconDescriptor.Click = string.Format("item:load(id={0})", item.ID);
                return gutterIconDescriptor;
            }

            return null;
        }
    }
}