using Sitecore.Data;

namespace Corlate.Feature.Metadata
{
    /// <summary>
    /// Defines the <see cref="References" />
    /// </summary>
    public struct References
    {
        /// <summary>
        /// Defines the <see cref="Templates" />
        /// </summary>
        public struct Templates
        {
            /// <summary>
            /// Defines the <see cref="Metadata" />
            /// </summary>
            public struct Metadata
            {
                /// <summary>
                /// Defines the ID
                /// </summary>
                public static readonly ID ID = new ID("{52267DC2-A9FF-4230-BD44-300D6BD6C71E}");

                /// <summary>
                /// Defines the <see cref="Fields" />
                /// </summary>
                public struct Fields
                {
                    /// <summary>
                    /// Defines the Title
                    /// </summary>
                    public static readonly ID Title = new ID("{02D564A0-6726-4545-BC48-A0BF51739ACD}");

                    /// <summary>
                    /// Defines the Description
                    /// </summary>
                    public static readonly ID Description = new ID("{67A4BA58-DCC5-462D-A5A4-049D8240E7A8}");

                    /// <summary>
                    /// Defines the Keywords
                    /// </summary>
                    public static readonly ID Keywords = new ID("{8B166472-8E4C-4077-A609-6719AF3B1F1F}");
                }
            }
        }
    }
}
