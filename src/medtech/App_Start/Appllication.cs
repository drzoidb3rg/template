using System.Web.Http;
using System.Web.Http.ValueProviders;
using System.Web.Routing;
using Autofac;
using OpenFileSystem.IO.FileSystems.Local.Win32;
using Poe;
using Poe.Webapi;
using Poe.Webapi.ValueProviders;
using Raven.Client;

namespace medtech.App_Start
{
    public class Application
    {
        public void Register(HttpConfiguration configuration)
        {
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; //Remove in actual production
            BootstrapWebApi.HasDependencies(r =>
            {
                r.Register(c => new Win32FileSystem())
                    .AsImplementedInterfaces()
                    .SingleInstance();

                r.Register(c =>  DocumentStoreFactory.GetDocumentStore())
                 .As<IDocumentStore>()
                 .SingleInstance();


                r.Register(c => new JsonNetValueProviderFactory())
                    .As<ValueProviderFactory>()
                    .SingleInstance();


            },
            new[]
            {
                typeof (Application).Assembly ,typeof(Register).Assembly
            }, 
            configuration);


            var registrations = BootstrapWebApi.HandlerRegistrations(new[]
                                                 {
                                                     typeof (Application).Assembly, typeof (Register).Assembly
                                                 });

            BootstrapWebApi.RegisterIntoAspNet(RouteTable.Routes, configuration, registrations, typeof(Application).Assembly);
            

        }

    }
}