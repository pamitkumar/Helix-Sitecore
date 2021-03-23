
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

    public class CoursesController : Controller
    {
        #region ACTION METHODS

        public ActionResult RenderCourseDetails()
        {
            CourseDetails viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = Sitecore.Context.Item;

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.CourseDetailsPage.ID)
                {
                    viewModel = new CourseDetails(renderingDatasourceItem);

                    if (viewModel.IsActive)
                    {
                        viewModel.Instructor = GetInstructor(viewModel.SelectedInstructorItemID);
                        viewModel.TimetableEvents = GetTimetableEvents(viewModel.InnerItem, References.Templates.CourseDetails.Fields.TimetableEvents);
                        viewModel.MoreCourses = GetMoreCourses(viewModel.InnerItem, References.Templates.CourseDetails.Fields.MoreCourses);

                    }
                    else
                    {
                        viewModel = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/CourseDetails.cshtml", viewModel);
        }

        public ActionResult RenderCourseArchive()
        {
            CourseArchive viewModel = new CourseArchive();

            try
            {
                bool showPrevButton = false;
                bool showNextButton = false;
                viewModel.CourseTeasers = GetCoursesByPage(Sitecore.Context.Item.ID.ToString(), "1", string.Empty, out showPrevButton, out showNextButton);

                if (viewModel.CourseTeasers != null && viewModel.CourseTeasers.Count > 0)
                {
                    viewModel.FirstAndLastCourseItemIDsInPage = viewModel.CourseTeasers.FirstOrDefault().InnerItem.ID.ToString() + "," + viewModel.CourseTeasers.LastOrDefault().InnerItem.ID.ToString();
                    viewModel.ShowPreviousButton = showPrevButton;
                    viewModel.ShowNextButton = showNextButton;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/CourseArchive.cshtml", viewModel);
        }

        public ActionResult GetCourseTeasers(string pageNumber, string referenceCourseItemIDs)
        {
            CourseArchive viewModel = new CourseArchive();

            try
            {
                bool showPrevButton = false;
                bool showNextButton = false;
                viewModel.CourseTeasers = GetCoursesByPage(string.Empty, pageNumber, referenceCourseItemIDs, out showPrevButton, out showNextButton);

                if (viewModel.CourseTeasers != null && viewModel.CourseTeasers.Count > 0)
                {
                    viewModel.FirstAndLastCourseItemIDsInPage = viewModel.CourseTeasers.FirstOrDefault().InnerItem.ID.ToString() + "," + viewModel.CourseTeasers.LastOrDefault().InnerItem.ID.ToString();
                    viewModel.ShowPreviousButton = showPrevButton;
                    viewModel.ShowNextButton = showNextButton;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return PartialView(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/_CourseTeasersListing.cshtml", viewModel);
        }

        public ActionResult RenderCourseTeasersCarousel()
        {
            CourseTeasersCarouselSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.CourseTeasersCarouselSource.ID)
                {
                    viewModel = new CourseTeasersCarouselSource(renderingDatasourceItem);

                    if (viewModel.IsActive)
                    {
                        Item courseArchivePageItem = SitecoreUtility.GetItem(new ID(viewModel.CourseArchivePageID));

                        if (courseArchivePageItem != null)
                        {
                            //get all the active course items
                            List<Item> coursePageItems = SitecoreUtility.GetItemsByTemplate(courseArchivePageItem, References.Templates.CourseDetailsPage.ID)
                                .Where(x => x.Fields[References.Templates.CourseDetails.Fields.IsActive].Value == "1").ToList();

                            if (coursePageItems != null && coursePageItems.Count > 0)
                            {
                                viewModel.CourseTeasers = new List<CourseDetails>();

                                foreach(Item item in coursePageItems)
                                {
                                    CourseDetails courseDetails = new CourseDetails(item);
                                    courseDetails.Instructor = GetInstructor(courseDetails.SelectedInstructorItemID);
                                    viewModel.CourseTeasers.Add(courseDetails);
                                }
                            }
                        }
                    }
                    else
                    {
                        viewModel = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/CourseTeasersCarousel.cshtml", viewModel);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Get the selected intructor
        /// </summary>
        /// <param name="selectedInstructorItemID"></param>
        /// <returns></returns>
        private Instructor GetInstructor(string selectedInstructorItemID)
        {
            Instructor instructor = null;

            if (!string.IsNullOrEmpty(selectedInstructorItemID))
            {
                Item instructorItem = SitecoreUtility.GetItem(new ID(selectedInstructorItemID));

                if (instructorItem != null)
                {
                    instructor = new Instructor(instructorItem);
                }
            }
            return instructor;
        }

        /// <summary>
        /// get the featured timetable events
        /// </summary>
        /// <param name="contentItem"></param>
        /// <param name="multilistFieldID"></param>
        /// <returns></returns>
        private List<TimetableEvent> GetTimetableEvents(Item contentItem, ID multilistFieldID)
        {
            List<TimetableEvent> timetableEvents = null;

            //get the list of items selected in the multilist field
            IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(contentItem, multilistFieldID);

            //check for each item if it is of valid template and active.If yes, then add it to the list
            if (featuredItems != null && featuredItems.Count > 0)
            {
                timetableEvents = new List<TimetableEvent>();

                foreach (Item featuredItem in featuredItems)
                {
                    if (featuredItem.TemplateID == References.Templates.TimetableEvent.ID)
                    {
                        TimetableEvent timetableEventItem = new TimetableEvent(featuredItem);

                        if (timetableEventItem != null && timetableEventItem.IsActive)
                        {
                            timetableEventItem.Instructor = GetInstructor(timetableEventItem.SelectedInstructorItemID);
                            timetableEvents.Add(timetableEventItem);
                        }
                    }
                }
            }

            return timetableEvents;
        }

        /// <summary>
        /// get the featured more courses
        /// </summary>
        /// <param name="contentItem"></param>
        /// <param name="multilistFieldID"></param>
        /// <returns></returns>
        private List<CourseDetails> GetMoreCourses(Item contentItem, ID multilistFieldID)
        {
            List<CourseDetails> moreCourses = null;

            //get the list of items selected in the multilist field
            IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(contentItem, multilistFieldID);

            //check for each item if it is of valid template and active.If yes, then add it to the list
            if (featuredItems != null && featuredItems.Count > 0)
            {
                moreCourses = new List<CourseDetails>();

                foreach (Item featuredItem in featuredItems)
                {
                    if (featuredItem.TemplateID == References.Templates.CourseDetailsPage.ID)
                    {
                        CourseDetails courseDetailsItem = new CourseDetails(featuredItem);

                        if (courseDetailsItem != null && courseDetailsItem.IsActive)
                        {
                            courseDetailsItem.Instructor = GetInstructor(courseDetailsItem.SelectedInstructorItemID);
                            moreCourses.Add(courseDetailsItem);
                        }
                    }
                }
            }

            return moreCourses;
        }

        /// <summary>
        /// gets courses by page
        /// </summary>
        /// <param name="parentItem">If its first page, parent item should be passed. All its child pages will be verified.</param>
        /// <param name="pageNumber">1 is for first page, 2 is for next page & 0 is for previous page</param>
        /// <param name="referenceCourseItemID">the course item id of the first or last course teaser on page</param>
        /// <returns></returns>
        private List<CourseDetails> GetCoursesByPage(string coursesRootItemID, string pageNumber, string referenceCourseItemIDs, out bool showPrevButton, out bool showNextButton)
        {
            showPrevButton = false;
            showNextButton = false;
            List<CourseDetails> lstCourses = new List<CourseDetails>();
            List<Item> featuredItems = new List<Item>();
            int maxTeasersPerPage = Constants.MaxCourseTeasersPerPage;
            int itemsInList = 0;

            if (pageNumber == "1" && !string.IsNullOrEmpty(coursesRootItemID))
            {
                Item coursesRootItem = SitecoreUtility.GetItem(new ID(coursesRootItemID));

                //get the list of courses for first page
                featuredItems = coursesRootItem != null ? SitecoreUtility.GetItemsByTemplate(coursesRootItem, References.Templates.CourseDetailsPage.ID) : null;

                if (featuredItems != null && featuredItems.Count > 0)
                {
                    foreach (Item featuredItem in featuredItems)
                    {
                        if (itemsInList < maxTeasersPerPage)
                        {
                            CourseDetails courseDetailsItem = new CourseDetails(featuredItem);

                            if (courseDetailsItem != null && courseDetailsItem.IsActive)
                            {
                                courseDetailsItem.Instructor = GetInstructor(courseDetailsItem.SelectedInstructorItemID);
                                lstCourses.Add(courseDetailsItem);
                                itemsInList++;
                            }
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(referenceCourseItemIDs))
            {
                string[] arrItemIDs = referenceCourseItemIDs.Split(',');

                if (arrItemIDs != null && arrItemIDs.Length > 0)
                {
                    Item referenceCourseItemFirst = !string.IsNullOrEmpty(arrItemIDs[0]) ? SitecoreUtility.GetItem(new ID(arrItemIDs[0])) : null;
                    Item referenceCourseItemLast = arrItemIDs.Length > 1 && !string.IsNullOrEmpty(arrItemIDs[1]) ? SitecoreUtility.GetItem(new ID(arrItemIDs[1])) : null;
                    Item siblingCourseItem = null;

                    while (itemsInList < maxTeasersPerPage)
                    {
                        if (pageNumber == "0")
                        {
                            //get previous page items
                            siblingCourseItem = referenceCourseItemFirst != null ? SitecoreUtility.GetPreceedingSiblingItemByTemplate(referenceCourseItemFirst, References.Templates.CourseDetailsPage.ID, References.Templates.CourseDetails.Fields.IsActive) : null;
                        }
                        else if (pageNumber == "2")
                        {
                            //get next page items
                            siblingCourseItem = referenceCourseItemLast != null ? SitecoreUtility.GetSucceedingSiblingItemByTemplate(referenceCourseItemLast, References.Templates.CourseDetailsPage.ID, References.Templates.CourseDetails.Fields.IsActive) : null;
                        }

                        if (siblingCourseItem != null)
                        {
                            referenceCourseItemFirst = siblingCourseItem;
                            referenceCourseItemLast = siblingCourseItem;
                            CourseDetails courseDetailsItem = new CourseDetails(siblingCourseItem);

                            if (courseDetailsItem != null && courseDetailsItem.IsActive)
                            {
                                courseDetailsItem.Instructor = GetInstructor(courseDetailsItem.SelectedInstructorItemID);
                                lstCourses.Add(courseDetailsItem);
                                itemsInList++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (lstCourses != null && lstCourses.Count > 0)
            {
                //If the 'Previous' button was clicked, then the sibling items are added with the first item in page as reference.
                //so this list should be reversed ideally.
                if (lstCourses.Count > 1 && pageNumber == "0")
                {
                    lstCourses.Reverse();
                }

                showPrevButton = SitecoreUtility.GetPreceedingSiblingItemByTemplate(lstCourses.FirstOrDefault().InnerItem, References.Templates.CourseDetailsPage.ID, References.Templates.CourseDetails.Fields.IsActive) != null;
                showNextButton = SitecoreUtility.GetSucceedingSiblingItemByTemplate(lstCourses.LastOrDefault().InnerItem, References.Templates.CourseDetailsPage.ID, References.Templates.CourseDetails.Fields.IsActive) != null;
            }

            return lstCourses;
        }



        #endregion
    }
}