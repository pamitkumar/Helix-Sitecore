
namespace Corlate.Feature.Navigation
{
    using Sitecore.Data;

    public struct References
    {
        public struct Templates
        {
            public struct NavigationItem
            {
                public static readonly ID ID = new ID("{ED14A430-C7D6-4418-B572-5C3DC5F79DB0}");

                public struct Fields
                {
                    public static readonly ID TargetURL = new ID("{691672E9-7CAE-4CDB-B8BA-F6C76E31E99B}");
                    public static readonly ID IsActive = new ID("{95A38828-059C-4F7A-9455-4661CBD52537}");
                }
            }

            public struct TopMenuSource
            {
                public static readonly ID ID = new ID("{4ADE0510-A396-4C88-8C10-E407DB8D9D28}");

                public struct Fields
                {
                    public static readonly ID IdentitySource = new ID("{E8396C68-5BED-4BBD-B691-862B7BA0163C}");
                }
            }
        }
    }
}