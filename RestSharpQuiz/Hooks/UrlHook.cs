using BoDi;
using RestSharp;
using System.Configuration;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Hooks
{
    [Binding]
    public sealed class UrlHook
    {
        public static RestClient restClient = new RestClient();
        public static IObjectContainer _objectContainer;
        public string envUrl = ConfigurationManager.AppSettings["basicEndpoint"];

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
