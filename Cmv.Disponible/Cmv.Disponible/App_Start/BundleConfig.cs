using System.Web;
using System.Web.Optimization;

namespace Cmv.Disponible
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));
            
             bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                         "~/Scripts/jquery-ui-{version}.js"));
             */
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/modernizr-*",
                "~/assets/libs/jquery/jquery-1-11-1-min.js",
                "~/assets/libs/bootstrap/js/bootstrap-min.js",
                "~/Scripts/jquery-unobtrusive-ajax.js",
                "~/Scripts/jquery-validate.js",
                "~/Scripts/jquery-validate-unobtrusive.js",
                "~/Scripts/jquery-validate-bootstrap-tooltip.js"
                ));

            // HAY QUE CHECAR POR QUE EXISTE UN ERROR SI SE AGREGA ES JS EB JQUERYVAL
            bundles.Add(new ScriptBundle("~/bundles/jqueryAjax").Include(
                             "~/Scripts/jquery.unobtrusive-ajax.js"
             ));

            bundles.Add(new ScriptBundle("~/assets/libs/Scriptprincipales").Include(
                "~/assets/libs/jquery/jquery-1-11-1-min.js",
                "~/assets/libs/bootstrap/js/bootstrap-min.js",
                "~/assets/libs/jqueryui/jquery-ui-1-10-4-custom-min.js",
                "~/assets/libs/jquery-ui-touch/jquery-ui-touch-punch-min.js",
                "~/assets/libs/jquery-detectmobile/detect.js",
                "~/assets/libs/jquery-notifyjs/notify-min.js",
                "~/assets/libs/jquery-notifyjs/styles/metro/notify-metro.js",
                "~/assets/libs/prettify/prettify.js",
                "~/assets/libs/bootstrap-fileinput/bootstrap-file-input.js",
                "~/assets/libs/bootstrap-select/bootstrap-select-min.js",
                "~/assets/libs/bootstrap-select2/select2-min.js",
                "~/assets/libs/magnific-popup/jquery-magnific-popup-min.js",
                 "~/assets/libs/ios7-switch/ios7-switch.js",
                 "~/assets/libs/jquery-slimscroll/jquery-slimscroll.js",
                 "~/assets/libs/jquery-icheck/icheck-min.js",
                 "~/assets/js/pages/index.js"
            ));

            bundles.Add(new ScriptBundle("~/assets/libs/ScriptMenu").Include(
                 "~/assets/libs/fastclick/fastclick.js",
                 "~/assets/libs/jquery-sparkline/jquery-sparkline.js",
                  "~/assets/js/init.js"
            ));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            #region CSS DEL TEMA
            bundles.Add(new StyleBundle("~/assets/libs/cssTema").Include(

                    "~/assets/libs/jqueryui/ui-lightness/jquery-ui-1_10_4_custom_min.css",
                    "~/assets/libs/bootstrap/css/bootstrap_min.css",
                    "~/assets/libs/font-awesome/css/font-awesome_min.css",
                    "~/assets/libs/fontello/css/fontello.css",
                    "~/assets/libs/animate-css/animate_min.css",
                    "~/assets/libs/nifty-modal/css/component.css",
                    "~/assets/libs/magnific-popup/magnific-popup.css",
                    "~/assets/libs/ios7-switch/ios7-switch.css",
                    "~/assets/libs/pace/pace.css",
                    "~/assets/libs/sortable/sortable-theme-bootstrap.css",
                    "~/assets/libs/bootstrap-datepicker/css/datepicker.css",
                    "~/assets/libs/jquery-icheck/skins/all.css",
                    "~/assets/libs/prettify/github.css",
                    "~/assets/libs/rickshaw/rickshaw_min.css",
                    "~/assets/libs/morrischart/morris.css",
                    "~/assets/libs/jquery-jvectormap/css/jquery-jvectormap-122.css",
                    "~/assets/libs/jquery-clock/clock.css",
                    "~/assets/libs/bootstrap-calendar/css/bic_calendar.css",
                    "~/assets/libs/sortable/sortable-theme-bootstrap.css",
                    "~/assets/libs/jquery-weather/simpleweather.css",
                    "~/assets/libs/bootstrap-xeditable/css/bootstrap-editable.css",
                    "~/assets/libs/jquery-notifyjs/styles/metro/notify-metro.css",
                    "~/assets/css/style.css",
                    "~/assets/css/style-responsive.css"
                    ,"~/CSS/loader.css"
                    ));
            #endregion




            #region SCRIPT PASADOS
            //bundles.Add(new ScriptBundle("~/assets/libs/scriptTema").Include(

            //        //"~/assets/libs/jquery-animate-numbers/jquery.animateNumbers.js",
            //        //"~/assets/libs/ios7-switch/ios7-switch.js",

            //        "~/assets/libs/jquery-blockui/jquery-blockUI.js",
            //        "~/assets/libs/bootstrap-bootbox/bootbox-min.js",
            //        "~/assets/libs/jquery-slimscroll/jquery-slimscroll.js",

            //        //"~/assets/libs/jquery-sparkline/jquery-sparkline.js",
            //        //"~/assets/libs/nifty-modal/js/classie.js",
            //        //"~/assets/libs/nifty-modal/js/modalEffects.js",
            //        //"~/assets/libs/sortable/sortable-min.js",

            //        "~/assets/libs/bootstrap-fileinput/bootstrap-file-input.js",
            //        "~/assets/libs/bootstrap-select/bootstrap-select-min.js",
            //        "~/assets/libs/bootstrap-select2/select2-min.js",
            //        "~/assets/libs/magnific-popup/jquery-magnific-popup-min.js",

            //        //"~/assets/libs/pace/pace-min.js",
            //        //"~/assets/libs/bootstrap-datepicker/js/bootstrap-datepicker.js",
            //        //"~/assets/libs/jquery-icheck/icheck-min.js",
            //        //"~/assets/libs/prettify/prettify.js",

            //        //"~/assets/js/init.js",
            //        //"~/assets/libs/d3/d3-v3.js",
            //        //"~/assets/libs/rickshaw/rickshaw-min.js",
            //        //"~/assets/libs/raphael/raphael-min.js",
            //        //"~/assets/libs/morrischart/morris-min.js",

            //        //"~/assets/libs/jquery-knob/jquery-knob.js",
            //        //"~/assets/libs/jquery-jvectormap/js/jquery-jvectormap-1-2-2-min.js",
            //        //"~/assets/libs/jquery-jvectormap/js/jquery-jvectormap-us-aea-en.js",
            //        //"~/assets/libs/jquery-clock/clock.js",
            //        //"~/assets/libs/jquery-easypiechart/jquery-easypiechart-min.js",
            //        //"~/assets/libs/jquery-weather/jquery-simpleWeather-2-6-min.js",
            //        //"~/assets/libs/bootstrap-xeditable/js/bootstrap-editable-min.js",
            //        //"~/assets/libs/bootstrap-calendar/js/bic_calendar-min.js",

            //        //"~/assets/libs/jquery-notifyjs/notify-min.js",
            //        //"~/assets/libs/jquery-notifyjs/styles/metro/notify-metro.js",
            //        //"~/assets/js/apps/calculator.js",
            //        //"~/assets/js/apps/todo.js",
            //        //"~/assets/js/apps/notes.js",
            //        "~/assets/js/pages/index.js"));

            //bundles.Add(new ScriptBundle("~/assets/InitsCharts").Include("~/assets/js/pages/morris-charts.js"));

            /* bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));*7

             /*bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                         "~/Content/themes/base/jquery.ui.core.css",
                         "~/Content/themes/base/jquery.ui.resizable.css",
                         "~/Content/themes/base/jquery.ui.selectable.css",
                         "~/Content/themes/base/jquery.ui.accordion.css",
                         "~/Content/themes/base/jquery.ui.autocomplete.css",
                         "~/Content/themes/base/jquery.ui.button.css",
                         "~/Content/themes/base/jquery.ui.dialog.css",
                         "~/Content/themes/base/jquery.ui.slider.css",
                         "~/Content/themes/base/jquery.ui.tabs.css",
                         "~/Content/themes/base/jquery.ui.datepicker.css",
                         "~/Content/themes/base/jquery.ui.progressbar.css",
                         "~/Content/themes/base/jquery.ui.theme.css"));*/
            #endregion
        }
    }
}