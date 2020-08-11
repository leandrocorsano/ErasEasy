using System.Web;
using System.Web.Optimization;

namespace ErasEasyLife
{
    public class BundleConfig
    {
        // Per altre informazioni sulla creazione di bundle, vedere https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilizzare la versione di sviluppo di Modernizr per eseguire attività di sviluppo e formazione. Successivamente, quando si è
            // pronti per passare alla produzione, usare lo strumento di compilazione disponibile all'indirizzo https://modernizr.com per selezionare solo i test necessari.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            // "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            /* bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/site.css"));*/
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/adminlte/plugins/fontawesome-free/css/all.min.css",
                      "~/adminlte/css/adminlte.min.css",
                      "~/adminlte/plugins/fullcalendar/main.min.css",
                      "~/adminlte/plugins/fullcalendar-daygrid/main.min.css",
                      "~/adminlte/plugins/fullcalendar-bootstrap/main.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/adminlte/js").Include(
             "~/adminlte/js/adminlte.min.js",
             "~/adminlte/plugins/fullcalendar/main.js",
             "~/adminlte/plugins/fullcalendar-daygrid/main.js",
             "~/adminlte/plugins/fullcalendar-bootstrap/main.js"
             ));
           
          
        }
    }
}
