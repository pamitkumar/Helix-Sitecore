using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Metadata.ViewModels
{
    public class MetadataViewModel
    {
        public string PageTitle { get; set; }
        public string SiteTitle { get; set; }
        public string Description { get; set; }
        public ICollection<string> KeywordsList { get; set; } = new List<string>();
        public string Title { get; set; }
        public ICollection<string> Robots { get; set; } = new List<string>();
        public ICollection<KeyValuePair<string, string>> CustomMetadata { get; set; } = new List<KeyValuePair<string, string>>();
    }
}