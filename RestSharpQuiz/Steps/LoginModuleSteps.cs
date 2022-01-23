using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharpQuiz.Models;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Steps
{
    [Binding]
    [Scope(Feature = "Login module")]
    public class LoginModuleSteps
    {
        private RestClient _restClinet;
        private RestRequest _restRequest;
        private RestResponse _restResponse;
        private User user;
        private Response response;
        private string signInUrl = "/users/login";
        private string envUrl = TestContext.Parameters["entryEndpoint"];

        public LoginModuleSteps(RestClient restClinet)
        {
            _restClinet = new RestClient(restClinet.BaseUrl + signInUrl);
            _restRequest = new RestRequest(Method.POST);
            user = new User(null, null, null, null, false);
        }

        [Given(@"Given User registers in system")]
        public void GivenGivenUserRegistersInSystem()
        {
            RestClient restClinet = new RestClient();

            if (string.IsNullOrEmpty(envUrl))
                envUrl = "https://learnandtest.herokuapp.com/quiz";

            RestRequest restRequest = new RestRequest(envUrl + "/users/register", Method.POST);
            user = user.CreateUser(user);
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);
            restClinet.Execute(restRequest);
        }

        [Given(@"User filled email and password correctly")]
        public void GivenUserFilledEmailAndPasswordCorrectly()
        {
            _restRequest.AddParameter("userEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [Given(@"User didn't fill email and password")]
        public void GivenUserDidnTFillEmailAndPassword()
        {
            _restRequest.AddParameter("userEmail", "");
            _restRequest.AddParameter("userPassword", "");
        }

        [Given(@"User fills incorrect Adres email")]
        public void GivenUserFillsIncorrectAdresEmail()
        {
            _restRequest.AddParameter("userEmail", "incorrectEmail");
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [Given(@"User fills incorrect Hasło")]
        public void GivenUserFillsIncorrectHaslo()
        {
            _restRequest.AddParameter("userEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", "incorrectPassword");
        }

        [Given(@"User filled too short password")]
        public void GivenUserFilledTooShortPassword()
        {
            _restRequest.AddParameter("userEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", "bad@");
        }

        [Given(@"User filled too long Adres email")]
        public void GivenUserFilledTooLongAdresEmail()
        {
            _restRequest.AddParameter("userEmail", "xv5XfZ1LXURRkaFvIEvzp7j8Fuj16dziBW9Pv8quGJsdQfOnyKV6hosAlndp2Au244" +
                "iHlJeHIaQHx2rqzcpyiwjqDywrzFz6CgCvUVVVngr2IkTfDQBsB88llpJYJWY2xbOdvLIBXQ2QOM65PlCBp0" +
                "TTVQX0lBvFLIAZg7kZNM2hQIN3bpvQ2GaacERotQuF3JPwlvUUr84B9h81Y4z0MmP1hrz1bDaoAzlU5j" +
                "Jx3ft9dCJLXUMUgig4rDDOv@email.com");
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [Given(@"User filled too long Hasło")]
        public void GivenUserFilledTooLongHaslo()
        {
            _restRequest.AddParameter("userEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", "tooLongWrongPasswordWith32Letters");
        }

        [When(@"Request sends to API")]
        public void WhenRequestSendsToAPI()
        {
            _restResponse = (RestResponse)_restClinet.Execute(_restRequest);
        }

        [Then(@"The server should return status (.*)")]
        public void ThenTheServerShouldReturnStatus(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_restResponse.StatusCode);
        }

        [Then(@"Response should return token")]
        public void ThenResponseShouldReturnToken()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.token, Is.Not.Null);
        }

        [Then(@"Response with error about missing data")]
        public void ThenResponseWithErrorAboutMissingData()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].userEmail, Is.EqualTo("Adres e-mail został wprowadzony niepoprawnie!"));
            Assert.That(response.validationError[1].userPassword, Is.EqualTo("Hasło jest za krótkie!"));
        }

        [Then(@"Response with error about incorrect Adres email")]
        public void ThenResponseWithErrorAboutIncorrectAdresEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].userEmail, Is.EqualTo("Adres e-mail został wprowadzony niepoprawnie!"));
        }

        [Then(@"Response with error about incorrect Hasło")]
        public void ThenResponseWithErrorAboutIncorrectHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.error, Is.EqualTo("Hasło jest nieprawidłowe!"));
        }

        [Then(@"Response with error about too short password")]
        public void ThenResponseWithErrorAboutTooShortPassword()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za krótkie!"));
        }

        [Then(@"Response with error about too long Adres email")]
        public void ThenResponseWithErrorAboutTooLongAdresEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].userEmail, Is.EqualTo("Wprowadzony adres e-mail jest za długi!"));
        }

        [Then(@"Response with error about too long Hasło")]
        public void ThenResponseWithErrorAboutTooLongHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za długie!"));
        }
    }
}
