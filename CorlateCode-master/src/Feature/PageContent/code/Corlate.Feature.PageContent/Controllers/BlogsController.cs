
namespace Corlate.Feature.PageContent.Controllers
{
    using Foundation.Common.Models;
    using Foundation.Common.Utilities;
    using Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class BlogsController : Controller
    {
        #region ACTION METHODS

        public ActionResult RenderBlog()
        {
            Blog viewModel = null;

            try
            {
                viewModel = GetBlogByPage("1", "");
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/Blog.cshtml", viewModel);
        }

        public ActionResult GetBlog(string pageNumber, string currentBlogItemID)
        {
            Blog viewModel = null;

            if (!string.IsNullOrEmpty(pageNumber) && !string.IsNullOrEmpty(currentBlogItemID))
            {
                try
                {
                    viewModel = GetBlogByPage(pageNumber, currentBlogItemID);
                }
                catch (System.Exception ex)
                {
                    LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
                }
            }

            return PartialView(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/_BlogDetails.cshtml", viewModel);
        }

        public ActionResult RenderPopularPosts()
        {
            PopularPostsSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.PopularPostsSource.ID)
                {
                    viewModel = new PopularPostsSource(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        viewModel.FeaturedBlogs = GetRelatedPosts(renderingDatasourceItem, References.Templates.PopularPostsSource.Fields.FeaturedBlogs);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/PopularPosts.cshtml", viewModel);
        }

        public ActionResult RenderTagsOrCategoriesSidebarFilter()
        {
            TagsOrCategoriesSidebarFilter viewModel = null;

            try
            {
                string blogArchivePageItemID = SitecoreUtility.GetRenderingParameters(References.Templates.TagsOrCategoriesSidebarFilter.ID, References.Templates.TagsOrCategoriesSidebarFilter.Fields.BlogArchivePage);

                if (!string.IsNullOrEmpty(blogArchivePageItemID))
                {
                    Item blogArchivePageItem = SitecoreUtility.GetItem(new ID(blogArchivePageItemID));

                    if (blogArchivePageItem != null)
                    {
                        viewModel = new TagsOrCategoriesSidebarFilter();
                        viewModel.SectionTitle = SitecoreUtility.GetRenderingParameters(References.Templates.TagsOrCategoriesSidebarFilter.ID, References.Templates.TagsOrCategoriesSidebarFilter.Fields.SectionTitle);
                        viewModel.DisplayItemsAsTags = SitecoreUtility.GetRenderingParameters(References.Templates.TagsOrCategoriesSidebarFilter.ID, References.Templates.TagsOrCategoriesSidebarFilter.Fields.DisplayItemsAsTags) == "1";
                        viewModel.BlogArchivePageURL = SitecoreUtility.GetItemURL(blogArchivePageItem);

                        if (!viewModel.DisplayItemsAsTags)
                        {
                            List<BlogCategory> categoriesUsedByBlogs = new List<BlogCategory>();
                            Item blogCategoriesFolderItem = SitecoreUtility.GetItem(References.Content.BlogCategoriesFolder);

                            if (blogCategoriesFolderItem != null && blogCategoriesFolderItem.HasChildren)
                            {
                                List<Item> activeCategories = SitecoreUtility.GetItemsByTemplate(blogCategoriesFolderItem, References.Templates.BlogCategory.ID)
                                    .Where(x => x.Fields[References.Templates.BlogCategory.Fields.IsActive].Value == "1").ToList();

                                if (activeCategories != null && activeCategories.Count > 0)
                                {
                                    foreach (Item category in activeCategories)
                                    {
                                        if (SitecoreUtility.HasReferrers(category, References.Templates.BlogPage.ID, References.Templates.Blog.Fields.IsActive))
                                        {
                                            BlogCategory blogCategory = new BlogCategory(category);
                                            categoriesUsedByBlogs.Add(blogCategory);
                                        }
                                    }

                                    viewModel.BlogCategories = categoriesUsedByBlogs;
                                }
                            }
                        }
                        else
                        {
                            List<BlogTag> tagsUsedByBlogs = new List<BlogTag>();
                            Item blogTagsFolderItem = SitecoreUtility.GetItem(References.Content.BlogTagsFolder);

                            if (blogTagsFolderItem != null && blogTagsFolderItem.HasChildren)
                            {
                                List<Item> activeTags = SitecoreUtility.GetItemsByTemplate(blogTagsFolderItem, References.Templates.BlogTag.ID)
                                    .Where(x => x.Fields[References.Templates.BlogTag.Fields.IsActive].Value == "1").ToList();

                                if (activeTags != null && activeTags.Count > 0)
                                {
                                    foreach (Item tag in activeTags)
                                    {
                                        if (SitecoreUtility.HasReferrers(tag, References.Templates.BlogPage.ID, References.Templates.Blog.Fields.IsActive))
                                        {
                                            BlogTag blogTag = new BlogTag(tag);
                                            tagsUsedByBlogs.Add(blogTag);
                                        }
                                    }

                                    viewModel.BlogTags = tagsUsedByBlogs;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/TagsOrCategoriesSidebarFilter.cshtml", viewModel);
        }

        public ActionResult RenderBlogArchive(string page, string cat, string tag)
        {
            BlogArchive viewModel = new BlogArchive();

            try
            {
                int totalPages;
                int selectedPage = Sitecore.MainUtil.GetInt(page, 1);
                viewModel.SelectedCategoryID = cat;
                viewModel.SelectedTagID = tag;
                viewModel.BlogArchivePageItemID = System.Convert.ToString(Sitecore.Context.Item.ID);
                viewModel.BlogTeasers = GetBlogTeasersByFilters(selectedPage, cat, tag, viewModel.BlogArchivePageItemID, out totalPages);
                viewModel.Pagination = GetPagination(selectedPage, totalPages);
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/BlogArchive.cshtml", viewModel);
        }

        public ActionResult GetBlogTeasers(string pageNumber, string categoryID, string tagID, string blogArchivePageItemID)
        {
            BlogArchive viewModel = new BlogArchive();

            try
            {
                int totalPages;
                int selectedPage = Sitecore.MainUtil.GetInt(pageNumber, 1);
                viewModel.SelectedCategoryID = categoryID;
                viewModel.SelectedTagID = tagID;
                viewModel.BlogArchivePageItemID = blogArchivePageItemID;
                viewModel.BlogTeasers = GetBlogTeasersByFilters(selectedPage, categoryID, tagID, blogArchivePageItemID, out totalPages);
                viewModel.Pagination = GetPagination(selectedPage, totalPages);
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/_BlogTeasersListing.cshtml", viewModel);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// get the featured related posts
        /// </summary>
        /// <param name="contentItem"></param>
        /// <param name="multilistFieldID"></param>
        /// <returns></returns>
        private List<Blog> GetRelatedPosts(Item contentItem, ID multilistFieldID)
        {
            List<Blog> relatedPosts = null;

            //get the list of items selected in the multilist field
            IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(contentItem, multilistFieldID);

            //check for each item if it is of valid template and active.If yes, then add it to the list
            if (featuredItems != null && featuredItems.Count > 0)
            {
                relatedPosts = new List<Blog>();

                foreach (Item featuredItem in featuredItems)
                {
                    if (featuredItem.TemplateID == References.Templates.BlogPage.ID)
                    {
                        Blog blogItem = new Blog(featuredItem);

                        if (blogItem != null && blogItem.IsActive)
                        {
                            relatedPosts.Add(blogItem);
                        }
                    }
                }
            }

            return relatedPosts;
        }

        /// <summary>
        /// gets courses by page
        /// </summary>
        /// <param name="parentItem">If its first page, parent item should be passed. All its child pages will be verified.</param>
        /// <param name="pageNumber">1 is for first page, 2 is for next page & 0 is for previous page</param>
        /// <param name="referenceCourseItemID">the course item id of the first or last course teaser on page</param>
        /// <returns></returns>
        private Blog GetBlogByPage(string pageNumber, string currentBlogItemID)
        {
            Blog blog = null;
            Item currentBlogItem = null;
            Item parentItem = null;
            List<Item> activeBlogItems = null;
            int indexOfCurrentItem = -1;

            if (!string.IsNullOrEmpty(pageNumber))
            {
                if (pageNumber == "1" && Sitecore.Context.Item.TemplateID == References.Templates.BlogPage.ID)
                {
                    blog = new Blog(Sitecore.Context.Item);
                    parentItem = Sitecore.Context.Item.Parent;
                    activeBlogItems = parentItem != null ? parentItem.Children
                        .Where(x => x.TemplateID == References.Templates.BlogPage.ID && x.Fields[References.Templates.Blog.Fields.IsActive].Value == "1")
                        .OrderBy(b => Sitecore.DateUtil.ToServerTime(Sitecore.DateUtil.IsoDateToDateTime(b.Fields[References.Templates.Blog.Fields.PostedDate].Value))).ToList() : null;

                    if (activeBlogItems != null && activeBlogItems.Count > 1)
                    {
                        indexOfCurrentItem = activeBlogItems.FindIndex(x => x.ID == Sitecore.Context.Item.ID);
                    }
                }
                else if (!string.IsNullOrEmpty(currentBlogItemID) && (pageNumber == "0" || pageNumber == "2"))
                {
                    currentBlogItem = SitecoreUtility.GetItem(new ID(currentBlogItemID));

                    parentItem = currentBlogItem != null ? currentBlogItem.Parent : null;
                    activeBlogItems = parentItem != null ? parentItem.Children
                        .Where(x => x.TemplateID == References.Templates.BlogPage.ID && x.Fields[References.Templates.Blog.Fields.IsActive].Value == "1")
                        .OrderBy(b => Sitecore.DateUtil.ToServerTime(Sitecore.DateUtil.IsoDateToDateTime(b.Fields[References.Templates.Blog.Fields.PostedDate].Value))).ToList() : null;

                    if (activeBlogItems != null && activeBlogItems.Count > 1)
                    {
                        indexOfCurrentItem = activeBlogItems.FindIndex(x => x.ID == currentBlogItem.ID);
                        Item siblingBlogItem = null;

                        if (pageNumber == "0" && indexOfCurrentItem > 0)
                        {
                            //get previous blog item
                            siblingBlogItem = activeBlogItems[indexOfCurrentItem - 1];
                        }
                        else if (pageNumber == "2" && indexOfCurrentItem < (activeBlogItems.Count - 1))
                        {
                            //get next blog item
                            siblingBlogItem = activeBlogItems[indexOfCurrentItem + 1];
                        }

                        if (siblingBlogItem != null)
                        {
                            blog = new Blog(siblingBlogItem);
                            indexOfCurrentItem = activeBlogItems.FindIndex(x => x.ID == siblingBlogItem.ID);
                        }
                    }
                }

                if (blog != null && blog.IsActive)
                {
                    blog.RelatedPosts = GetRelatedPosts(blog.InnerItem, References.Templates.Blog.Fields.RelatedPosts);
                    blog.PreviousBlogURL = indexOfCurrentItem > 0 ? SitecoreUtility.GetItemURL(activeBlogItems[indexOfCurrentItem - 1]) : "";
                    blog.NextBlogURL = indexOfCurrentItem < (activeBlogItems.Count - 1) ? SitecoreUtility.GetItemURL(activeBlogItems[indexOfCurrentItem + 1]) : "";
                }
                else
                {
                    blog = null;
                }
            }

            return blog;
        }

        /// <summary>
        /// get pagewise blog teasers based on the filters like category & tag
        /// </summary>
        /// <param name="selectedPage"></param>
        /// <param name="categoryID"></param>
        /// <param name="tagID"></param>
        /// <param name="blogArchivePageItemID"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        private List<Blog> GetBlogTeasersByFilters(int selectedPage, string categoryID, string tagID, string blogArchivePageItemID,  out int totalPages)
        {
            List<Item> blogPageItems = new List<Item>();
            List<Blog> blogsByPage = new List<Blog>();
            totalPages = 1;            
            Item blogArchivePageItem = !string.IsNullOrEmpty(blogArchivePageItemID) ? SitecoreUtility.GetItem(new ID(blogArchivePageItemID)) : null;

            if (blogArchivePageItem != null && blogArchivePageItem.HasChildren)
            {
                blogPageItems = SitecoreUtility.GetItemsByTemplate(blogArchivePageItem, References.Templates.BlogPage.ID)
                    .Where(x => x.Fields[References.Templates.Blog.Fields.IsActive].Value == "1").ToList();

                if (blogPageItems != null && blogPageItems.Count > 0)
                {
                    int maxTeasersPerPage = Constants.MaxBlogTeasersPerPage;

                    if (!string.IsNullOrEmpty(categoryID))
                    {
                        Item categoryItem = SitecoreUtility.GetItem(new ID(categoryID));

                        if (categoryItem != null && categoryItem.Fields[References.Templates.BlogCategory.Fields.IsActive].Value == "1")
                        {
                            blogPageItems = blogPageItems.Where(x => x.Fields[References.Templates.Blog.Fields.BlogCategories].Value.Contains(categoryID)).ToList();
                        }
                    }
                    else if (!string.IsNullOrEmpty(tagID))
                    {
                        Item tagItem = SitecoreUtility.GetItem(new ID(tagID));

                        if (tagItem != null && tagItem.Fields[References.Templates.BlogTag.Fields.IsActive].Value == "1")
                        {
                            blogPageItems = blogPageItems.Where(x => x.Fields[References.Templates.Blog.Fields.BlogTags].Value.Contains(tagID)).ToList();
                        }
                    }

                    if (blogPageItems != null && blogPageItems.Count > 0)
                    {
                        foreach (Item item in blogPageItems)
                        {
                            blogsByPage.Add(new Blog(item));
                        }                        
                    }

                    if (blogsByPage != null && blogsByPage.Count > 0)
                    {
                        blogsByPage = blogsByPage.OrderByDescending(x => x.PostedDateTime).ToList();
                        blogsByPage = SitecoreUtility.GetPagewiseRecords<Blog>(blogsByPage, maxTeasersPerPage, selectedPage, out totalPages);
                    }
                }
            }

            return blogsByPage;
        }

        /// <summary>
        /// get the pagination details
        /// </summary>
        /// <param name="selectedPage"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        private Pagination GetPagination(int selectedPage, int totalPages)
        {
            Pagination pagination = new Pagination();
            //assuming that the max. numbered boxes to show in pagination is 5. (i.e the no. of boxes in pagination that have integers)
            int maxBoxesWithIntegersInPagination = 5;
            int maxBoxesToLeftOfSelectedPageInPagination = 2;
            
            pagination.TotalPages = totalPages;
            pagination.SelectedPage = selectedPage > totalPages ? totalPages : selectedPage;

            
            if (totalPages <= maxBoxesWithIntegersInPagination)
            {
                pagination.FirstPageNumberInPagination = 1;
                pagination.LastPageNumberInPagination = totalPages;
            }
            else
            {
                if (selectedPage == totalPages)
                {
                    pagination.FirstPageNumberInPagination = totalPages - (maxBoxesWithIntegersInPagination - 1);
                    pagination.LastPageNumberInPagination = selectedPage;
                }
                else
                {
                    int firstPage = selectedPage - maxBoxesToLeftOfSelectedPageInPagination;
                    firstPage = (totalPages - firstPage) < (maxBoxesWithIntegersInPagination - 1) ? firstPage - 1 : firstPage;
                    pagination.FirstPageNumberInPagination = firstPage;
                    pagination.LastPageNumberInPagination = firstPage + (maxBoxesWithIntegersInPagination - 1);
                }
            }

            return pagination;
        }

        #endregion
    }
}