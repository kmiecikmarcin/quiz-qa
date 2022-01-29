using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharpQuiz.Models;
using System;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Steps
{
    [Binding]
    [Scope(Feature = "Change user password")]
    public class ChangeUserPasswordSteps
    {
        private RestClient _restClinet;
        private RestRequest _restRequest;
        private RestResponse _restResponse;
        private User user;
        private Response response;
        private string updateUserPassword = "/users/password";
        private string userToken;
        private string envUrl = TestContext.Parameters["entryEndpoint"];

        public ChangeUserPasswordSteps(RestClient restClinet)
        {
            _restClinet = new RestClient(restClinet.BaseUrl + updateUserPassword);
            _restRequest = new RestRequest(Method.PATCH);
            user = new User(null, null, null, null, false);
        }

        [Given(@"User registers into system and logs in")]
        public void GivenUserRegistersIntoSystemAndLogsIn()
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

        [Given(@"User filled data correctly")]
        public void GivenUserFilledDataCorrectly()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", "newPassword@");
            _restRequest.AddParameter("confirmNewUserPassword", "newPassword@");
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

        [Then(@"Response with a new token")]
        public void ThenResponseWithANewToken()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.token, Is.Not.Null);
        }

        [Given(@"User didn't fill data")]
        public void GivenUserDidnTFillData()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", "");
            _restRequest.AddParameter("confirmNewUserPassword", "");
            _restRequest.AddParameter("userPassword", "");
        }

        [Then(@"The server should return status (.*)")]
        public void ThenTheServerShouldReturnStatus(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_restResponse.StatusCode);
        }

        [Then(@"Response with error message about missing data")]
        public void ThenResponseWithErrorMessageAboutMissingData()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].newUserPassword, Is.EqualTo("Hasło jest za krótkie!"));
            Assert.That(response.validationError[1].confirmNewUserPassword, Is.EqualTo("Hasła są różne!"));
            Assert.That(response.validationError[2].userPassword, Is.EqualTo("Hasło jest za krótkie!"));
        }

        [Given(@"User filled password which is not assigned to account")]
        public void GivenUserFilledPasswordWhichIsNotAssignedToAccount()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", user.userPassword);
            _restRequest.AddParameter("confirmNewUserPassword", user.userPassword);
            _restRequest.AddParameter("userPassword", "wrongPassword@");
        }

        [Then(@"Response with error message about wrong password")]
        public void ThenResponseWithErrorMessageAboutWrongPassword()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.error, Is.EqualTo("Wprowadzone hasło jest nieprawidłowe!"));
        }

        [Given(@"User filled new password '(.*)' with '(.*)'")]
        public void GivenUserFilledNewPasswordWith(string typeOfMistake, string data)
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", data);
            _restRequest.AddParameter("confirmNewUserPassword", data);
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [Then(@"Response with error message about incorrect the new password based on '(.*)'")]
        public void ThenResponseWithErrorMessageAboutIncorrectTheNewPasswordBasedOn(string typeOfMistake)
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            if (typeOfMistake == "'without special key'") Assert.That(response.validationError[0].newUserPassword, Is.EqualTo("Hasło nie zawiera minimum jednego znaku specjalnego!"));
            if (typeOfMistake == "'too short'" || typeOfMistake == "'without data'") Assert.That(response.validationError[0].newUserPassword, Is.EqualTo("Hasło jest za krótkie!"));
            if (typeOfMistake == "'too long'") Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za długie!"));
        }

        [Given(@"User filled confirm password field incorrect")]
        public void GivenUserFilledConfirmPasswordFieldIncorrect()
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", "newPassword@");
            _restRequest.AddParameter("confirmNewUserPassword", "differentPassword@");
            _restRequest.AddParameter("userPassword", user.userPassword);
        }

        [Then(@"Response with error message about wrong confirmation")]
        public void ThenResponseWithErrorMessageAboutWrongConfirmation()
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            Assert.That(response.validationError[0].confirmNewUserPassword, Is.EqualTo("Hasła są różne!"));
        }

        [Given(@"User filled password '(.*)' with '(.*)'")]
        public void GivenUserFilledPasswordWith(string typeOfMistake, string data)
        {
            _restRequest.AddHeader("Authorization", "Bearer " + userToken);
            _restRequest.AddParameter("newUserPassword", "newPassword@");
            _restRequest.AddParameter("confirmNewUserPassword", "newPassword@");
            _restRequest.AddParameter("userPassword", data);
        }

        [Then(@"Response with error message about incorrect password based on '(.*)'")]
        public void ThenResponseWithErrorMessageAboutIncorrectPasswordBasedOn(string typeOfMistake)
        {
            response = JsonConvert.DeserializeObject<Response>(_restResponse.Content);
            if (typeOfMistake == "'without special key'") Assert.That(response.error, Is.EqualTo("Wprowadzone hasło jest nieprawidłowe!"));
            if (typeOfMistake == "'too short'" || typeOfMistake == "'without data'") Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za krótkie!"));
            if (typeOfMistake == "'too long'") Assert.That(response.validationError[0].userPassword, Is.EqualTo("Hasło jest za długie!"));
        }
    }
}
