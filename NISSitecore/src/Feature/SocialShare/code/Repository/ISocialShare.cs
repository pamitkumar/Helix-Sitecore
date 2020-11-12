using NISSitecore.Feature.SocialShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.SocialShare.Repository
{
    public interface ISocialShare
    {
        List<SocialItemProperty> GetSocialShareButtonList(string rootItemId);
    }
}
