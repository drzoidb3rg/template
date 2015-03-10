using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using Autofac;
using OpenFileSystem.IO;
using OpenFileSystem.IO.FileSystems.Local.Win32;
using Poe;
using Poe.Webapi.ValueProviders;
using Raven.Client;
using Test.Infrastructure;

namespace medtech.test
{
    public class Tester
    {
        public static WebTest<object, MedtechRuntime> AsAnon(Action<HttpRequestMessage> requestDefaults = null, params string[] roles)
        {
            return WebTest<object, MedtechRuntime>(requestDefaults);
        }

        static WebTest<TResource, TRuntime> WebTest<TResource, TRuntime>(Action<HttpRequestMessage> requestDefaults = null) where TRuntime : System.Web.HttpApplication
        {
            TestingConfig.Configure(c => c.AppInitialised = true);
            var test = new WebTest<TResource, TRuntime>(c =>
            {
                c.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            },
            r =>
            {
                r.Register((c) => new Win32FileSystem())
                    .As<IFileSystem>()
                    .SingleInstance();
                r.Register(c => new JsonNetValueProviderFactory())
                    .As<ValueProviderFactory>()
                    .SingleInstance();
                r.Register((c) => DocumentStoreFactory.GetDocumentStore(true))
                    .As<IDocumentStore>()
                    .SingleInstance()
                    .ExternallyOwned();

            },
            requestDefaults);

            return test;
        }
    }
}
