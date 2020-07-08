using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Maps;
using NISSitecore.Feature.Navigation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.ORM
{
    public class GlassMappings
    {

    }

    public class NavigationRootMap : SitecoreGlassMap<INavigationRoot>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.NavigationRoot.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class LinkMap : SitecoreGlassMap<ILink>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.Link.TemplateId);
                config.Field(f => f.Link).FieldId(Constants.Link.Fields.Link);
                config.AutoMap();
            });
        }
    }

    public class LinkMenuItemMap : SitecoreGlassMap<ILinkMenuItem>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.LinkMenuItem.TemplateId);
                config.Field(f => f.Icon).FieldId(Constants.LinkMenuItem.Fields.Icon);
                config.Field(f => f.DividerBefore).FieldId(Constants.LinkMenuItem.Fields.DividerBefore);
                config.AutoMap();
            });
        }
    }

    public class NavigableMap : SitecoreGlassMap<INavigable>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.Navigable.TemplateId);
                config.Field(f => f.NavigationTitle).FieldId(Constants.Navigable.Fields.NavigationTitle);
                config.Field(f => f.ShowChildren).FieldId(Constants.Navigable.Fields.ShowChildren);
                config.Field(f => f.ShowInNavigation).FieldId(Constants.Navigable.Fields.ShowInNavigation);
                config.AutoMap();
            });
        }
    }
}