using BoDi;
using RestSharp;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace RestSharpQuiz.Hooks
{
    [Binding]
    public sealed class UrlHook
    {
        public static RestClient restClient = new RestClient();
        public static IObjectContainer _objectContainer;
        public string envUrl = TestContext.Parameters["entryEndpoint"];

        public UrlHook(IObjectContainer objectContainer)
        {           
            _objectContainer = objectContainer;

            if (string.IsNullOrEmpty(envUrl))
                envUrl = "https://localhost:3000/quiz";

            restClient = new RestClient(envUrl);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _objectContainer.RegisterInstanceAs<RestClient>(restClient);
        }
    }
}
