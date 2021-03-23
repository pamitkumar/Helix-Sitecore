
namespace Corlate.Feature.PageContent.Models
{
    using Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public class Blog : CustomItem
    {
        public Blog(Item innerItem) : base(innerItem) { }

        public string ImageID
        {
            get
            {
                return References.Templates.Blog.Fields.Image.ToString();
            }
        }

        public string TitleID
        {
            get
            {
                return References.Templates.Blog.Fields.Title.ToString();
            }
        }

        public string AuthorID
        {
            get
            {
                return References.Templates.Blog.Fields.Author.ToString();
            }
        }

        public string Author
        {
            get
            {
                return InnerItem.Fields[References.Templates.Blog.Fields.Author].Value;
            }
        }

        public string PostedDateID
        {
            get
            {
                return References.Templates.Blog.Fields.PostedDate.ToString();
            }
        }

        public DateTime PostedDateTime
        {
            get
            {
                string postedDate = InnerItem.Fields[References.Templates.Blog.Fields.PostedDate].Value;

                if (!string.IsNullOrEmpty(postedDate))
                {
                    return Sitecore.DateUtil.ToServerTime(Sitecore.DateUtil.IsoDateToDateTime(postedDate));
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string PostedDate
        {
            get
            {
                string postedDate = string.Empty;
                string isoDate = InnerItem.Fields[References.Templates.Blog.Fields.PostedDate].Value;

                if (!string.IsNullOrEmpty(isoDate))
                {
                    DateTime selectedDate = Sitecore.DateUtil.ToServerTime(Sitecore.DateUtil.IsoDateToDateTime(isoDate));

                    if (selectedDate != null)
                    {
                        postedDate = selectedDate.ToString("dd MMMM yyyy");
                    }
                }

                return postedDate;
            }
        }


        public string BriefDescriptionID
        {
            get
            {
                return References.Templates.Blog.Fields.BriefDescription.ToString();
            }
        }

        public string ContentID
        {
            get
            {
                return References.Templates.Blog.Fields.Content.ToString();
            }
        }

        public string BlogCategoriesID
        {
            get
            {
                return References.Templates.Blog.Fields.BlogCategories.ToString();
            }
        }


        public string BlogTagsID
        {
            get
            {
                return References.Templates.Blog.Fields.BlogTags.ToString();
            }
        }

        public string RelatedPostsLabelID
        {
            get
            {
                return References.Templates.Blog.Fields.RelatedPostsLabel.ToString();
            }
        }

        public List<Blog> RelatedPosts { get; set; }

        public string ReadMoreLabelID
        {
            get
            {
                return References.Templates.Blog.Fields.ReadMoreLabel.ToString();
            }
        }

        public string ReadMoreLabel
        {
            get
            {
                return InnerItem.Fields[References.Templates.Blog.Fields.ReadMoreLabel].Value;
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.Blog.Fields.IsActive].Value == "1";
            }
        }

        public string PageURL
        {
            get
            {
                return SitecoreUtility.GetItemURL(InnerItem);
            }
        }

        public string PreviousBlogURL { get; set; }

        public string NextBlogURL { get; set; }

    }

}
