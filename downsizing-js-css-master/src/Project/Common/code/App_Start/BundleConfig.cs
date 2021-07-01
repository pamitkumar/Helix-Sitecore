using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Sitecore.Common.Website.App_Start.BundleConfig), "DemoStart")]

namespace Sitecore.Common.Website.App_Start
{
    public class BundleConfig
    {
        public static void DemoStart()
        {
            try
            {
                var bundles = BundleTable.Bundles;
                BundleTable.EnableOptimizations = true;

                var lstjs = ResourceConfig.GetListJavaScript();
                var bundlepath = "~/bundle/";

                foreach (var item in lstjs)
                {
                    var directoryjss = item.MyProperty;

                    DirectoryInfo d = new DirectoryInfo(HttpContext.Current.Server.MapPath(directoryjss));
                    FileInfo[] Files = d.GetFiles("*.js");
                    foreach (FileInfo file in Files)
                    {
                        var jss = new ScriptBundle(bundlepath + file.Name).Include(directoryjss + "/" + file.Name);
                        bundles.Add(jss);
                    }
                }

                var lstStyles = ResourceConfig.GetListStyles();

                foreach (var stylepath in lstStyles)
                {
                    var directorycss = stylepath.MyProperty;

                    DirectoryInfo d1 = new DirectoryInfo(HttpContext.Current.Server.MapPath(directorycss));
                    FileInfo[] Files1 = d1.GetFiles("*.css");
                    foreach (FileInfo file in Files1)
                    {
                        var cssdynamic = new StyleBundle(bundlepath + file.Name).Include(directorycss + "/" + file.Name);
                        bundles.Add(cssdynamic);
                    }
                }
            }

       
            catch (Exception e)
            {
                Log.Error(e.Message, typeof(BundleConfig));
            }

        }
   
    }
}