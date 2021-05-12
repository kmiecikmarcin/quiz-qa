using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharpQuiz.Models;
using System;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Steps
{
    [Binding]
    [Scope(Feature = "Registration form")]
    public class RegistrationFormSteps
    {
        RestClient restClient = new RestClient();
        RestRequest restRequest = new RestRequest("https://learnandtest.herokuapp.com/quiz/users/register", Method.POST);
        RestResponse restResponse;
        User user;
        Response response;

        [Given(@"User filled data correctly")]
        public void GivenUserFilledDataCorrectly()
        {
            user = new User(null, null, null, null, false);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data")]
        public void GivenUserDidnTFillData()
        {
            user = new User("", "", "", "", false);
        }

        [Given(@"Users filled data with incorrect Adres email")]
        public void GivenUsersFilledDataWithIncorrectAdresEmail()
        {
            user = new User("useremail.com", null, null, null, false);
            user.CreateUser(user);
        }

        [Given(@"Users filled data with incorrect Hasło")]
        public void GivenUsersFilledDataWithIncorrectHaslo()
        {
            user = new User(null, "wrongpassword", null, null, true);
            user.CreateUser(user);
        }

        [Given(@"Users filled data with incorrect Płeć")]
        public void GivenUsersFilledDataWithIncorrectPlec()
        {
            user = new User(null, null, null, "wrongGender", true);
            user.CreateUser(user);
        }

        [Given(@"Users filled data with incorrect Weryfikacja")]
        public void GivenUsersFilledDataWithIncorrectWeryfikacja()
        {
            user = new User(null, null, null, null, null);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data in Adres email")]
        public void GivenUserDidnTFillDataInAdresEmail()
        {
            user = new User("", null, null, null, true);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data in Hasło")]
        public void GivenUserDidnTFillDataInHaslo()
        {
            user = new User(null, "", null, null, true);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data in Powtórz hasło")]
        public void GivenUserDidnTFillDataInPowtorzHaslo()
        {
            user = new User(null, null, "", null, true);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data in Płeć")]
        public void GivenUserDidnTFillDataInPlec()
        {
            user = new User(null, null, null, "", true);
            user.CreateUser(user);
        }

        [Given(@"User didn't fill data in Weryfikacja")]
        public void GivenUserDidnTFillDataInWeryfikacja()
        {
            user = new User(null, null, null, null, null);
            user.CreateUser(user);
        }

        [Given(@"User filled too short Hasło")]
        public void GivenUserFilledTooShortHaslo()
        {
            user = new User(null, "Wr@ng", null, null, true);
            user.CreateUser(user);
        }

        [Given(@"User filled too short Płeć")]
        public void GivenUserFilledTooShortPlec()
        {
            user = new User(null, null, null, "K", true);
            user.CreateUser(user);
        }

        [Given(@"User filled too long Adres email")]
        public void GivenUserFilledTooLongAdresEmail()
        {
            user = new User("xv5XfZ1LXURRkaFvIEvzp7j8Fuj16dziBW9Pv8quGJsdQfOnyKV6hosAlndp2Au244" +
                "iHlJeHIaQHx2rqzcpyiwjqDywrzFz6CgCvUVVVngr2IkTfDQBsB88llpJYJWY2xbOdvLIBXQ2QOM65PlCBp0" +
                "TTVQX0lBvFLIAZg7kZNM2hQIN3bpvQ2GaacERotQuF3JPwlvUUr84B9h81Y4z0MmP1hrz1bDaoAzlU5j" +
                "Jx3ft9dCJLXUMUgig4rDDOv@email.com", null, null, null, true);
            user.CreateUser(user);
        }

        [Given(@"User filled too long Hasło")]
        public void GivenUserFilledTooLongHaslo()
        {
            user = new User(null, "tooLongPasswordWhichIsWrong12345@", null, null, true);
            user.CreateUser(user);
        }

        [Given(@"User filled too long Płeć")]
        public void GivenUserFilledTooLongPlec()
        {
            user = new User(null, null, null, "tooLongAndWrongUserGender", true);
            user.CreateUser(user);
        }

        [When(@"Request sends to API")]
        public void WhenRequestSendsToAPI()
        {
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);
        }
        
        [Then(@"The server should return positive status 200")]
        public void ThenTheServerShouldReturnPositiveStatus()
        {
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Assert.AreEqual(201, (int)restResponse.StatusCode);
        }
        
        [Then(@"Response with message about successfully process")]
        public void ThenResponseWithMessageAboutSuccessfullyProcess()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.messages.message, Is.EqualTo("Rejestracja przebiegła pomyślnie!"));
        }

        [Then(@"The server should return status 400")]
        public void ThenTheServerShouldReturnStatus()
        {
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Assert.AreEqual(400, (int)restResponse.StatusCode);
        }

        [Then(@"Response with error about missing data")]
        public void ThenResponseWithErrorAboutMissingData()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors, Is.Not.Null);
        }

        [Then(@"Response with error about incorrect Adres email")]
        public void ThenResponseWithErrorAboutIncorrectAdresEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_email, Is.EqualTo("Adres e-mail został wprowadzony niepoprawnie!"));
        }

        [Then(@"Response with error about incorrect Hasło")]
        public void ThenResponseWithErrorAboutIncorrectHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_password, Is.EqualTo("Hasło nie zawiera minimum jednego znaku specjalnego!"));
        }

        [Then(@"Response with error about incorrect Płeć")]
        public void ThenResponseWithErrorAboutIncorrectPlec()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_gender, Is.EqualTo("Podano błędną wartość!"));
        }

        [Then(@"Response with error about incorrect Weryfikacja")]
        public void ThenResponseWithErrorAboutIncorrectWeryfikacja()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_verification, Is.EqualTo("Wprowadzona wartość jest nieprawidłowa!"));
        }

        [Then(@"Response with error about empty Adres email")]
        public void ThenResponseWithErrorAboutEmptyAdresEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_email, Is.EqualTo("Adres e-mail został wprowadzony niepoprawnie!"));
        }

        [Then(@"Response with error about empty Hasło")]
        public void ThenResponseWithErrorAboutEmptyHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_password, Is.EqualTo("Hasło jest za krótkie!"));
        }

        [Then(@"Response with error about empty Powtórz hasło")]
        public void ThenResponseWithErrorAboutEmptyPowtorzHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].confirm_password, Is.EqualTo("Hasła sa różne!"));
        }

        [Then(@"Response with error about empty Płeć")]
        public void ThenResponseWithErrorAboutEmptyPlec()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_gender, Is.EqualTo("Wprowadzone dane są za krótkie!"));
        }

        [Then(@"Response with error about empty Weryfikacja")]
        public void ThenResponseWithErrorAboutEmptyWeryfikacja()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_verification, Is.EqualTo("Wprowadzona wartość jest nieprawidłowa!"));
        }

        [Then(@"Response with error about too short Hasło")]
        public void ThenResponseWithErrorAboutTooShortHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_password, Is.EqualTo("Hasło jest za krótkie!"));
        }

        [Then(@"Response with error about too short Płeć")]
        public void ThenResponseWithErrorAboutTooShortPlec()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_gender, Is.EqualTo("Podano błędną wartość!"));
        }

        [Then(@"Response with error about too long Adres email")]
        public void ThenResponseWithErrorAboutTooLongAdresEmail()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_email, Is.EqualTo("Wprowadzony adres e-mail jest za długi!"));
        }

        [Then(@"Response with error about too long Hasło")]
        public void ThenResponseWithErrorAboutTooLongHaslo()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_password, Is.EqualTo("Hasło jest za długie!"));
        }

        [Then(@"Response with error about too long Płeć")]
        public void ThenResponseWithErrorAboutTooLongPlec()
        {
            response = JsonConvert.DeserializeObject<Response>(restResponse.Content);
            Assert.That(response.validationErrors[0].user_gender, Is.EqualTo("Wprowadzone dane sa za długie!"));
        }
    }
}
