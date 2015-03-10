using System.Net;
using Test;
using Xunit;

namespace medtech.test.Features.Home
{
    public class HomeTests
    {
        [Fact]
        public void CanGetHomePage()
        {
            using (var _ = Tester.AsAnon(Accept.Any)
                .Get(""))
            {
                _.Response.StatusCodeShouldBe(HttpStatusCode.OK);
                _.Response.CompareWithAcceptedText();
            }
        }
    }
}