using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharpQuiz.Models;
using System;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Steps
{
    [Binding]
    [Scope(Feature = "Change user email")]
    public class ChangeUserEmailSteps
    {
        private RestClient _restClinet;
        private RestRequest _restRequest;
        private RestResponse _restResponse;
        private User user;
        private Response response;
        private string updateUserEmail = "/users/email";
        private string userToken;
        private string envUrl = TestContext.Parameters["entryEndpoint"];

        public ChangeUserEmailSteps(RestClient restClinet)
        {
            _restClinet = new RestClient(restClinet.BaseUrl + updateUserEmail);
            _restRequest = new RestRequest(Method.PATCH);
            user = new User(null, null, null, null, false);
        }

        [Given(@"User registers into system and logs in")]
        public void GivenUserRegistersIntoSystem()
        {
            RestClient restClinet = new RestClient();
            RestResponse restResponse = new RestResponse();

            if (string.IsNullOrEmpty(envUrl))
                envUrl = "https://learnandtest.herokuapp.com/quiz";

            RestRequest restRequest = new RestRequest(envUrl + "/users/register", Method.POST);
            user = user.CreateUser(user);

            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);

            restResponse = (RestResponse)_restClinet.Execute(restRequest);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            restRequest = new RestRequest(envUrl + "/users/login", Method.POST);

            restRequest.AddParameter("userEmail", user.userEmail);
            restRequest.AddParameter("userPassword", user.userPassword);

            restResponse = (RestResponse)_restClinet.Execute(restRequest);
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            userToken = response.token;
        }
        
        [Given(@"User filled correctly data")]
        public void GivenUserFilledCorrectlyData()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserEmail", user.GenerateNewUserEmail());
            _restRequest.AddParameter("userPassword", user.userPassword);
        }
        
        [Given(@"User filled email which is assigned to account")]
        public void GivenUserFilledEmailWhichIsAssignedToAccount()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", user.userPassword);
        }
        
        [Given(@"User filled password which is incorrect")]
        public void GivenUserFilledPasswordWhichIsIncorrect()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserEmail", user.GenerateEmail());
            _restRequest.AddParameter("userPassword", "passwd");
        }
        
        [Given(@"User filled email '(.*)' with '(.*)'")]
        public void GivenUserFilledEmailWith(string typeOfMistake, string data)
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserEmail", data);
            _restRequest.AddParameter("userPassword", user.userPassword);
        }
        
        [Given(@"User filled password '(.*)' with '(.*)'")]
        public void GivenUserFilledPasswordWith(string typeOfMistake, string data)
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", data);
        }

        [Given(@"User filled correctly data without authorization")]
        public void GivenUserFilledCorrectlyDataWithoutAuthorization()
        {
            _restRequest.AddParameter("newUserEmail", user.userEmail);
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [When(@"Request sends to API")]
        public void WhenRequestSendsToAPI()
        {
            _restResponse = (RestResponse)_restClinet.Execute(_restRequest);
        }

        [Then(@"The server should return status (.*) on success")]
        public void ThenTheServerShouldReturnStatusOnSuccess(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_restResponse.StatusCode);
        }

        [Then(@"The server should return status (.*)")]
        public void ThenTheServerShouldReturnStatus(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_restResponse.StatusCode);
        }
        
        [Then(@"Response with a new token")]
        public void ThenResponseWithSuccessMessage()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.token, Is.Not.Null);
        }
        
        [Then(@"Response with message about already assigned email")]
        public void ThenResponseWithMessageAboutAlreadyAssignedEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.error, Is.EqualTo("Wprowadzony email już istnieje!"));
        }
        
        [Then(@"Response with message about password")]
        public void ThenResponseWithMessageAboutPassword()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);

            Assert.That(response.error, Is.EqualTo("Wprowadzone hasło jest nieprawidłowe!"));
        }
        
        [Then(@"Response with message about incorrect email based on (.*)")]
        public void ThenResponseWithMessageAboutIncorrectEmail(string typeOfMistake)
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            if (typeOfMistake != "'too long'") Assert.That(response.validationError[0].newUserEmail, Is.EqualTo("Adres e-mail został wprowadzony niepoprawnie!"));
            else
            {
                Assert.That(response.validationError[0].newUserEmail, Is.EqualTo("Wprowadzony adres e-mail jest za długi!"));
            }
        }
        
        [Then(@"Response with message about incorrect password based on (.*)")]
        public void ThenResponseWithMessageAboutIncorrectPassword(string typeOfMistake)
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            if(typeOfMistake == "'which is too short'" || typeOfMistake == "'empty'") Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za krótkie!"));
            else if (typeOfMistake == "'which is too long'") Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za długie!"));
        }

        [Then(@"Response with error message about authorization")]
        public void ThenResponseWithErrorMessageAboutAuthorization()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.error, Is.EqualTo("Nie udało się przeprowadzić procesu uwierzytelniania!"));
        }
    }
}
