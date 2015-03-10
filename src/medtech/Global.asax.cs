using System;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Poe;
using Poe.Webapi;
using Serilog;
using medtech.App_Start;

namespace medtech
{

    public class MedtechRuntime : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var app = new Application();
            app.Register(GlobalConfiguration.Configuration);
            Task.Factory.StartNew(AsyncPostInit);
        }


        protected void AsyncPostInit()
        {
            try
            {
                TestingConfig.Configure(c => c.AppInitialised = true);
                var appstart = BootstrapWebApi.Context.Resolve<PostAppStart>();
                appstart.Run();
            }
            catch (Exception ex)
            {
                Log.Error("Exception {@exception}", ex);
            }
        }

        protected void Application_OnError() // work out if fireing 
        {
            var logId = Guid.NewGuid().ToString("N").Substring(24);
            var exception = Server.GetLastError();
            Log.Error("Exception {@exception} with error code {logId}", exception, logId);
        }
    }
}