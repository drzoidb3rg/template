using Poe.Hypermedia;

namespace medtech.Features.Home.Hypermedia
{
    public class HomeHypermedia : HypermediaControl
    {
        public string Title { get; set; }

        public HomeHypermedia()
        {
            Title = "Medtech";
        }
    }
}