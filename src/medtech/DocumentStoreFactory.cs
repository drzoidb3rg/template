using System.Reflection;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Client.Util;
using medtech.App_Start;

namespace medtech
{
    public class DocumentStoreFactory
    {
        public static IDocumentStore GetDocumentStore(bool tests = false)
        {
            if (tests)
            {
                var store = new EmbeddableDocumentStore
                {
                    RunInMemory = true,
                    UseEmbeddedHttpServer = false,
                    Conventions = GetConvention()
                    
                };
                store.Initialize();
                IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(Application)), store);
                return store;
            }

            var documentStore = new DocumentStore
                {
                    ConnectionStringName = "ExternalRavenDB",
                    Conventions = GetConvention(),
                };
            documentStore.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStore);
            return documentStore;
        }


        public static DocumentConvention GetConvention()
        {
            var documentConvention = new DocumentConvention
            {
                FindTypeTagName = t => Inflector.Pluralize(t.Name).ToLowerInvariant()
            };

            return documentConvention;
        }
    }
}
