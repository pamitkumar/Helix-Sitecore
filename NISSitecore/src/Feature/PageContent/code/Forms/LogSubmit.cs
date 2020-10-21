using System.Linq;
using NISSitecore.Feature.PageContent.Forms.Uitlity;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using static System.FormattableString;

namespace NISSitecore.Feature.PageContent.Forms
{
    public class LogSubmit : SubmitActionBase<string>
    {
        public LogSubmit(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }
        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
        }
        /// <summary>
        /// Executes the action with the specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="formSubmitContext">The form submit context.</param>
        /// <returns>
        ///   <c>true</c> if the action is executed correctly; otherwise <c>false</c>
        /// </returns>
        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
           
            if (!formSubmitContext.HasErrors)
            {
                var firstName = Helper.GetValue(Helper.GetFieldByName("First Name", formSubmitContext.Fields));
                var lastName = Helper.GetValue(Helper.GetFieldByName("Last Name", formSubmitContext.Fields));
                var email = Helper.GetValue(Helper.GetFieldByName("Email", formSubmitContext.Fields));               
                PostSubmitter post = new PostSubmitter();
               
                post.Url = "http://seeker.dice.com/jobsearch/servlet/JobSearch";
                post.PostItems.Add("first_name", firstName);
                post.PostItems.Add("last_name", lastName);
                post.PostItems.Add("email", email);
               
                post.Type = PostSubmitter.PostTypeEnum.Post;
                string result = post.Post();
                Logger.Info(Invariant($"Form {formSubmitContext.FormId} submitted successfully."), this);
            }
            else
            {
                Logger.Warn(Invariant($"Form {formSubmitContext.FormId} submitted with errors: {string.Join(", ", formSubmitContext.Errors.Select(t => t.ErrorMessage))}."), this);
            }

            return true;
        }
    }
}