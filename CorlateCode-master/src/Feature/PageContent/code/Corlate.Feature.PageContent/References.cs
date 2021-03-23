
using Sitecore.Data;

namespace Corlate.Feature.PageContent
{
    public struct References
    {
        public struct Content
        {
            ///sitecore/content/Corlate/University/Featured Items/Blog Categories
            public static readonly ID BlogCategoriesFolder = new ID("{DD5645A7-3CC8-4602-AC7D-56FA432DABCD}");
            ///sitecore/content/Corlate/University/Featured Items/Blog Tags
            public static readonly ID BlogTagsFolder = new ID("{A27E6595-8D05-4F01-AE7D-59E0AA912CF8}");
            ///sitecore/content/Corlate/University/Form Data/Contact Us Form Data
            public static readonly ID ContactUsFormDataFolder = new ID("{E8277912-B6F7-488D-943D-C19B55D17D13}");
        }

        public struct Templates
        {
            public struct Banner
            {
                public static readonly ID ID = new ID("{04FDB6CB-9599-44A9-9A16-28A5C4023273}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{E9C755F2-59A1-4841-A4B5-3B97A7393FBA}");
                    public static readonly ID Image = new ID("{F16378AD-247B-4101-A158-F56F8E55F5FF}");
                    public static readonly ID IsActive = new ID("{F3A6C090-E860-4453-9F9B-0247D2CC9DE2}");
                }
            }

            public struct ImageGalleryItem
            {
                public static readonly ID ID = new ID("{399A23B4-473F-4930-B83E-AB506EB94806}");

                public struct Fields
                {
                    public static readonly ID Thumbnail = new ID("{884635DE-D6F7-4774-9A81-2B8491B7B9F1}");
                    public static readonly ID Title = new ID("{6A2E78F2-37B2-4C13-BE58-D7CC277CA777}");
                    public static readonly ID ViewLabel = new ID("{78739142-715C-46B5-AC4D-F2904E840FAA}");
                    public static readonly ID IsActive = new ID("{603833E8-37B0-4CF4-BB19-D05523C6FADD}");
                    public static readonly ID Image = new ID("{9D1AF50F-8C1D-49F5-9726-ABD3650EA9CD}");
                }
            }

            public struct ImageGallerySource
            {
                public static readonly ID ID = new ID("{CAAA5AE5-5529-4F47-98E7-D8A8880F718A}");

                public struct Fields
                {
                    public static readonly ID GalleryItems = new ID("{AC960183-2BFC-4568-A54E-7C142914AF81}");
                    public static readonly ID IsActive = new ID("{5D592839-74A5-4586-9AB7-C0AF331AE6DD}");
                }
            }

            public struct FooterSocialLink
            {
                public static readonly ID ID = new ID("{A29AF761-F20A-42C1-9D2D-C303AC455936}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{B10AA20B-836E-4412-8D99-ECD336186D90}");
                    public static readonly ID TargetURL = new ID("{FFA2A519-8F3D-4037-8CCD-95ACE642FAB7}");
                    public static readonly ID CSSClass = new ID("{6C1B48C6-C879-49C2-8C08-06288CB99C64}");
                    public static readonly ID IsActive = new ID("{AC254443-9189-4847-94B3-F9B76FDFA70C}");
                }
            }

            public struct FooterSource
            {
                public static readonly ID ID = new ID("{490E5E71-5F79-419C-A52E-6244A9911DC8}");

                public struct Fields
                {
                    public static readonly ID SocialLinks = new ID("{C0BE4D51-910F-4787-AACC-D5D4F838A1D8}");
                    public static readonly ID IsActive = new ID("{9BA54862-AAB0-428F-A7C9-9AFE1CB99668}");
                    public static readonly ID TopSectionTitle = new ID("{DE39D4B0-792A-4E5C-85CF-F810DE2391A4}");
                    public static readonly ID IdentitySource = new ID("{0ADB389F-3FB6-4751-A5C5-6D7272084944}");
                }
            }

            public struct Instructor
            {
                public static readonly ID ID = new ID("{F62C162E-4C7E-4C20-B9D1-43ABA9E31E25}");

                public struct Fields
                {
                    public static readonly ID InstructorName = new ID("{63EB36DA-EA74-4F2A-A263-5BBB7E5B2576}");
                    public static readonly ID Designation = new ID("{5059F5EC-BB4D-4D7C-9F08-E205207059BF}");
                    public static readonly ID IsActive = new ID("{12C57CE1-2ECE-4E8C-B80C-02B25D09F091}");
                    public static readonly ID Image = new ID("{5A638015-9F0C-41B7-B50B-1016C4786A03}");
                }
            }

            public struct TimetableEvent
            {
                public static readonly ID ID = new ID("{EF6956D6-3978-47C9-A8A9-E32DFC84694A}");

                public struct Fields
                {
                    public static readonly ID CourseTitle = new ID("{24FE577A-0E10-4F40-9903-E67DD7626183}");
                    public static readonly ID Instructor = new ID("{EF96E07C-D444-42E2-9C23-B7628290E583}");
                    public static readonly ID Timing = new ID("{120E66A4-E9D9-406F-B010-B17D44ACF9F1}");
                    public static readonly ID IsActive = new ID("{90A5E047-3FD3-466C-9277-000B36D4465A}");
                }
            }

            public struct CourseDetailsPage
            {
                public static readonly ID ID = new ID("{89A2E1F8-6707-424B-8721-03F1420FD6B3}");
            }

            public struct CoursesPage
            {
                public static readonly ID ID = new ID("{42362310-101F-47B7-A3AE-115751B5A500}");
            }

            public struct CourseDetails
            {
                public static readonly ID ID = new ID("{D0C124E6-AA78-4ABD-848D-EC389BCFC6DA}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{1A7802E6-4DF6-4685-9F85-A01123326FD2}");
                    public static readonly ID TeaserImage = new ID("{7C43D740-9467-4606-9677-A29553E93D25}");
                    public static readonly ID MainImage = new ID("{660A2DDE-8693-40F0-B0E1-BBF7C5A7D639}");
                    public static readonly ID Instructor = new ID("{6492FAB4-46D6-48FB-AB3D-7B765CFAC44B}");
                    public static readonly ID Cost = new ID("{F1576467-F899-41EB-8664-C6B23D09CFF0}");
                    public static readonly ID Duration = new ID("{E0065277-AF27-4798-959C-9CBC9232800C}");
                    public static readonly ID ContentHeading = new ID("{B684ED39-0D0F-45D2-AD2B-42CC862E9ECF}");
                    public static readonly ID BriefDescription = new ID("{DEE119D9-6E61-4659-9B11-3EE5C01ED966}");
                    public static readonly ID Content = new ID("{6595B3EC-A2AE-4843-B102-CFA3260DA7B1}");
                    public static readonly ID ViewCourseLabel = new ID("{3EEDD79E-02A9-42F8-9712-03CA957DC1EA}");
                    public static readonly ID TimetableColumn1Label = new ID("{D241503A-E839-43F8-8CD9-D2133BA4D116}");
                    public static readonly ID TimetableColumn2Label = new ID("{F8425506-D160-477C-A52D-B99E103975A8}");
                    public static readonly ID TimetableColumn3Label = new ID("{0FA9AD3E-BEA2-4C1A-8208-3A90F3C48281}");
                    public static readonly ID TimetableEvents = new ID("{F5D6F137-1BD9-4953-9A78-79514A3FABBC}");
                    public static readonly ID MoreCoursesSectionLabel = new ID("{FDBF2114-E239-4B94-AAD9-BB39AD238811}");
                    public static readonly ID MoreCourses = new ID("{C0B2EED8-53D0-4C0B-8745-9791954B4B27}");
                    public static readonly ID IsActive = new ID("{A595A873-D821-4167-92D7-34B4CEE544DE}");
                }
            }

            public struct BlogCategory
            {
                public static readonly ID ID = new ID("{887C3260-8BFB-4661-A079-DCEEF164D2D4}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{2873DCDF-840B-4079-B3F4-942DB3CC08BE}");
                    public static readonly ID IsActive = new ID("{966D70BB-F2A7-42B4-868D-1C7E1EC4B2C5}");
                }
            }

            public struct BlogTag
            {
                public static readonly ID ID = new ID("{EB02CF18-A4C3-4C3E-94BF-4393BCFF8419}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{9FAB7004-370C-4E1C-8A07-4E282E73184C}");
                    public static readonly ID IsActive = new ID("{595110EE-2D63-4D47-8D36-12A407314F8D}");
                }
            }

            public struct Blog
            {
                public static readonly ID ID = new ID("{9DD2B952-D4B8-4DEF-A6F3-54489D1A4397}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{DCEEA491-7E4A-4B9A-8128-ABEC0394F844}");
                    public static readonly ID PostedDate = new ID("{612ECD8B-2DC9-49AA-AB5F-D22F525E62A8}");
                    public static readonly ID ReadMoreLabel = new ID("{341497A9-2F56-473D-8357-46BA8AAA6749}");
                    public static readonly ID Author = new ID("{27CF9E28-D52C-415B-A76F-AE9237A3D125}");
                    public static readonly ID Image = new ID("{2626D545-4370-44CB-A4C4-FD39C97EEA12}");
                    public static readonly ID BriefDescription = new ID("{7BAAC592-6268-4A98-91D5-E0F59C4E52F6}");
                    public static readonly ID Content = new ID("{B8E99C6D-6EFC-4E9B-945C-EBABB1454681}");
                    public static readonly ID BlogCategories = new ID("{0285B66F-1AB5-419C-B8C2-621A2AAF0B93}");
                    public static readonly ID BlogTags = new ID("{F0F6C084-C718-4E5C-B32F-92AF00C74931}");
                    public static readonly ID RelatedPostsLabel = new ID("{3B4CC2CF-2D69-493A-80FB-1A63E6AB0011}");
                    public static readonly ID RelatedPosts = new ID("{08B2C0E2-0964-4C9C-AB0E-6AC9D2176732}");
                    public static readonly ID IsActive = new ID("{6C5989A7-6549-49C1-B3FD-BBB10771AC43}");
                }
            }

            public struct BlogPage
            {
                public static readonly ID ID = new ID("{20CE4D23-C35A-4CF4-AA62-7D51FD88804C}");
            }

            public struct PopularPostsSource
            {
                public static readonly ID ID = new ID("{1267AA6E-CF0E-468B-B3FF-9358A25B6233}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{3FD3EBB2-E098-4CF4-A004-F9DE28EFE139}");
                    public static readonly ID FeaturedBlogs = new ID("{00FBBB5A-4B6E-4757-B2B5-D73D7CABC43C}");
                    public static readonly ID IsActive = new ID("{948608CC-F7D2-44DC-867E-4850AD85101C}");
                }
            }

            public struct TagsOrCategoriesSidebarFilter
            {
                public static readonly ID ID = new ID("{F246A765-4567-4C58-A9BC-63329010132E}");

                public struct Fields
                {
                    public static readonly ID SectionTitle = new ID("{CDA189A4-7509-4116-B7F6-034AE4D3E776}");
                    public static readonly ID DisplayItemsAsTags = new ID("{639B1F32-E7FC-473E-AB33-8944A7415D2E}");
                    public static readonly ID BlogArchivePage = new ID("{786422D0-C08A-4320-B45B-89372B1F82BE}");
                }
            }

            public struct BlogArchiveSettings
            {
                public static readonly ID ID = new ID("{5B2C1084-5EF1-406C-BDD7-8B1CB9C9E009}");

                public struct Fields
                {
                    public static readonly ID BlogArchivePage = new ID("{68E075E8-DF4C-4893-BC22-520DB047DB4D}");
                }
            }

            public struct BannerCarouselSlide
            {
                public static readonly ID ID = new ID("{F7B65B97-3B2A-4A21-9061-5C1DD72D37C5}");

                public struct Fields
                {
                    public static readonly ID Image = new ID("{ADBA5E6B-38D3-4527-BBF1-C150239A28AB}");
                    public static readonly ID Title = new ID("{510273CD-526A-432E-9259-A03FF0047D73}");
                    public static readonly ID Description = new ID("{E32926D6-D635-4C95-AA57-6DC4EECD3F5E}");
                    public static readonly ID CTA = new ID("{5E127C48-D41F-4C2C-9320-0C845321468A}");
                    public static readonly ID IsActive = new ID("{0FCEE2C4-1C5B-4953-8D3A-745E65938B46}");
                }
            }

            public struct BannerCarouselSource
            {
                public static readonly ID ID = new ID("{A83EC6CC-98AB-4400-A43C-1194AC46C8D9}");

                public struct Fields
                {
                    public static readonly ID Slides = new ID("{3E5EAE17-1F64-4ECF-980F-733FB799455E}");
                    public static readonly ID IsActive = new ID("{BDE7E2D7-848F-48C4-9606-BC9B0ED0FAA2}");
                }
            }

            public struct RichTextSection
            {
                public static readonly ID ID = new ID("{969DEBCB-DF3B-453F-A534-C69B3F9D260A}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{73D1661E-E50F-4C54-B7B8-7B40DD5DF6DD}");
                    public static readonly ID Description = new ID("{3FA75981-BD32-4D40-B329-63B9A6CB58D0}");
                    public static readonly ID IsActive = new ID("{E6AAD5ED-D611-413E-AD4F-308B8E690BA8}");
                }
            }

            public struct Facet
            {
                public static readonly ID ID = new ID("{E152A74B-D355-44C3-B6B0-53898546C757}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{3340851C-7B6D-4DFC-AA8F-B4773F7C55EC}");
                    public static readonly ID Description = new ID("{C4A19EFA-2E3D-42D0-84CC-2518CDE50658}");
                    public static readonly ID ImageCSSClass = new ID("{8029A722-4626-4868-82FF-28C0FCCAB5B2}");
                    public static readonly ID IsActive = new ID("{7C7F62D3-4F57-43EB-B29F-2D2F853AB006}");
                }
            }

            public struct FacetsSource
            {
                public static readonly ID ID = new ID("{03720720-785E-41B2-9E74-A40B15182DEE}");

                public struct Fields
                {
                    public static readonly ID Facets = new ID("{E78C76A5-9381-4A7F-BFCA-F68829EA3D43}");
                    public static readonly ID IsActive = new ID("{EF843F61-7041-4093-BF39-4984C46D2DAC}");
                    public static readonly ID Title = new ID("{35C94A43-9952-4EF1-B954-54E94F969052}");
                }
            }

            public struct CourseTeasersCarouselSource
            {
                public static readonly ID ID = new ID("{4C3E2C47-7109-453D-87D4-9E8B8CE0A412}");

                public struct Fields
                {
                    public static readonly ID IsActive = new ID("{3FD2ED95-CFD1-4E47-8017-2174BB49C55D}");
                    public static readonly ID Title = new ID("{DF8964B9-B041-4305-8115-EDA1534B58F0}");
                    public static readonly ID CourseArchivePage = new ID("{FC940B28-75AF-4FFE-8402-B7357CD5D049}");
                }
            }

            public struct UserTestimonial
            {
                public static readonly ID ID = new ID("{13460155-D3DA-4922-96EE-E3FAB9E8AA77}");

                public struct Fields
                {
                    public static readonly ID Name = new ID("{07857D68-C114-4166-96A0-86F3A108407D}");
                    public static readonly ID Designation = new ID("{4B2BE496-2C65-4D92-9FCF-35E6B77FC436}");
                    public static readonly ID Image = new ID("{D205BAE8-7EEE-4E44-B212-8B887AA34D78}");
                    public static readonly ID Testimonial = new ID("{0244EFDD-1A7A-4B84-874D-320E44F29728}");
                    public static readonly ID IsActive = new ID("{03C0B1B7-D775-4823-8B1B-B6881FB5C7BC}");
                }
            }

            public struct TestimonialsSource
            {
                public static readonly ID ID = new ID("{914D796A-3A86-4256-B32E-49C36DDF94EA}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{87743740-2719-4C35-928D-A7F10EE3DFCA}");
                    public static readonly ID Testimonials = new ID("{F6ADE5E4-6C5B-4DF3-9556-CB8E0146DC94}");
                    public static readonly ID IsActive = new ID("{4A93374C-9990-4C16-AF74-4E1F13E8968C}");
                }
            }

            public struct ContactUsFormSource
            {
                public static readonly ID ID = new ID("{DDC46D14-1B08-4B49-AB51-B478A70290AC}");

                public struct Fields
                {
                    public static readonly ID Title = new ID("{C7F614D5-34E3-4A39-8147-E9BCCB750CE9}");
                    public static readonly ID Description = new ID("{CFBD6A18-A07E-43C4-9E1F-ED9423C74107}");
                    public static readonly ID Address = new ID("{DC213AC9-4093-4657-9CB6-1B30E49A6627}");
                    public static readonly ID SocialNetworkLinks = new ID("{523ADD67-822B-47F1-A885-3BA744214B9F}");
                    public static readonly ID IsActive = new ID("{851EFFB4-9B64-49FD-B70F-1D7F51BBEE45}");
                }
            }

            public struct ContactUsFormData
            {
                public static readonly ID ID = new ID("{C92D034F-512E-453A-A684-5CE937356D6C}");

                public struct Fields
                {
                    public static readonly ID FullName = new ID("{89B512DE-6348-45DC-848D-852A3063C695}");
                    public static readonly ID Email = new ID("{B10CF585-023A-4CBD-8CDB-FE45375D136A}");
                    public static readonly ID Subject = new ID("{4647DD87-3888-47E6-838B-F7C9054DE90F}");
                    public static readonly ID Comments = new ID("{18B68F37-0927-47FA-92BC-61A466B17957}");
                }
            }


        }
    }
}