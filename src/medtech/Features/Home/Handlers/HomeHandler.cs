using System.Net.Http;
using Poe;
using Raven.Client;
using medtech.Features.Home.Hypermedia;

namespace medtech.Features.Home.Handlers
{
    public class HomeHandler : Handles<HomeHypermedia>
    {
        static Register route = () => "";
        readonly IDocumentStore store;

        public HomeHandler(IDocumentStore store)
        {
            this.store = store;
        }

        public HttpResponseMessage Get()
        {
            return OK(new HomeHypermedia());
        }

    }
}