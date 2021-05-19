using BoDi;
using RestSharp;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Hooks
{
    [Binding]
    public sealed class UrlHook
    {
        public static RestClient restClient = new RestClient();
        public static IObjectContainer _objectContainer;
        public string Url = "https://learnandtest.herokuapp.com/quiz";

        public UrlHook(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            restClient = new RestClient(Url);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _objectContainer.RegisterInstanceAs<RestClient>(restClient);
        }
    }
}