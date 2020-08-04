using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class RenderingParametersExtensions
    {
        public static string ToQueryString(this RenderingParameters parameters)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> parameter in parameters)
                pairs.Add(parameter.Key, parameter.Value);
            return pairs.ToQueryString();
        }

        public static string ToJson(this RenderingParameters renderingParameters)
        {
            var keyValues = renderingParameters as IEnumerable<KeyValuePair<string, string>>;
            if (keyValues != null)
            {
                var renderingParams = new JObject();
                foreach (var keyValue in keyValues)
                {
                    renderingParams.Add(keyValue.Key, keyValue.Value);
                }

                return JsonConvert.SerializeObject(renderingParams);
            }

            return string.Empty;

        }
    }
}