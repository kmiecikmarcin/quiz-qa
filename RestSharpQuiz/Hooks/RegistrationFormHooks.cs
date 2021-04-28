using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Hooks
{
    [Binding]
    public sealed class RegistrationFormHooks
    {
        string Url { get; set; }

        [BeforeScenario]
        public void BeforeScenario()
        {
            new RestClient();
            new RestRequest(Url, Method.POST);

            if (Url == null)
                Url = "localhost:3000/quiz/users/register";
        }
    }
}
