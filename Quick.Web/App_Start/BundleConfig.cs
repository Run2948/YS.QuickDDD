using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Quick.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                //"~/Content/common/bootstrap-select/js/bootstrap-select.min.js",
                //"~/Content/common/datetimepicker/js/bootstrap-datetimepicker.js",
                //"~/Content/common/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js",
                //"~/Content/common/bootstrap-fileinput/js/fileinput.js",
                //"~/Content/common/bootstrap-fileinput/js/fileinput_locale_zh.js",
                //"~/Scripts/respond.js",
                "~/Scripts/bootstrap.js"
            ));

            bundles.Add(new StyleBundle("~/Content/common/css").Include(
                //"~/Content/common/bootstrap-fileinput/css/fileinput.css",
                //"~/Content/common/bootstrap-select/css/bootstrap-select.min.css",
                //"~/Content/common/datetimepicker/css/bootstrap-datetimepicker.css",
                "~/Content/common/bootstrap.css"
            ));
        }
    }
}