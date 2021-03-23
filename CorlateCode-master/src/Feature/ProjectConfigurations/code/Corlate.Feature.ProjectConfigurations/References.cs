
namespace Corlate.Feature.ProjectConfigurations
{
    using Sitecore.Data;

    public struct References
    {
        public struct Templates
        {
            public struct PageSettings
            {
                public static readonly ID ID = new ID("{DB4406C2-0BFE-4E55-84DF-E97DB0D22E54}");

                public struct Fields
                {
                    public static readonly ID IdentitySource = new ID("{A26C011D-D6E4-439D-8E96-B9F0412D3D93}");
                }
            }

            public struct Identity
            {
                public static readonly ID ID = new ID("{85B2FC09-5FDC-4773-A980-F7AC334607E0}");

                public struct Fields
                {
                    public static readonly ID LogoTitle = new ID("{D6F4E247-28E6-4844-B8F0-227E4FE31E5D}");
                    public static readonly ID LogoTargetURL = new ID("{6AC2BA6F-AD5B-4C47-B6EE-27F84B4B0F5D}");
                    public static readonly ID FooterCopyrightText = new ID("{F32647C7-1BB4-4E94-892C-DEA2A0F55C8E}");
                    public static readonly ID Favicon = new ID("{69CF435E-CEE4-471A-B9C0-72F0B9CA1C3D}");
                }
            }
        }
    }
}

