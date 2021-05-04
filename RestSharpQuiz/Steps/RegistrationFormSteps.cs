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
        RestRequest restRequest = new RestRequest("http://localhost:3000/quiz/users/register", Method.POST);
        RestResponse restResponse;
        User user;
        Response response;

        [Given(@"User filled data correctly")]
        public void GivenUserFilledDataCorrectly()
        {
            user = new User("", "", "", "", false);
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
            user = new User("useremail.com", "", "", "", false);
            user.CreateUser(user);
        }

        [Given(@"Users filled data with incorrect Hasło")]
        public void GivenUsersFilledDataWithIncorrectHaslo()
        {
            
        }

        [Given(@"Users filled data with incorrect Płeć")]
        public void GivenUsersFilledDataWithIncorrectPlec()
        {
            
        }

        [Given(@"Users filled data with incorrect Weryfikacja")]
        public void GivenUsersFilledDataWithIncorrectWeryfikacja()
        {
            
        }

        [Given(@"User didn't fill data in Adres email")]
        public void GivenUserDidnTFillDataInAdresEmail()
        {
            
        }

        [Given(@"User didn't fill data in Hasło")]
        public void GivenUserDidnTFillDataInHaslo()
        {
            
        }

        [Given(@"User didn't fill data in Powtórz hasło")]
        public void GivenUserDidnTFillDataInPowtorzHaslo()
        {
            
        }

        [Given(@"User didn't fill data in Płeć")]
        public void GivenUserDidnTFillDataInPlec()
        {
            
        }

        [Given(@"User didn't fill data in Weryfikacja")]
        public void GivenUserDidnTFillDataInWeryfikacja()
        {
            
        }

        [Given(@"User filled too short Adres email")]
        public void GivenUserFilledTooShortAdresEmail()
        {
            
        }

        [Given(@"User filled too short Hasło")]
        public void GivenUserFilledTooShortHaslo()
        {
            
        }

        [Given(@"User filled too short Płeć")]
        public void GivenUserFilledTooShortPlec()
        {
            
        }

        [Given(@"User filled too long Adres email")]
        public void GivenUserFilledTooLongAdresEmail()
        {
            
        }

        [Given(@"User filled too long Hasło")]
        public void GivenUserFilledTooLongHaslo()
        {
            
        }

        [Given(@"User filled too long Płeć")]
        public void GivenUserFilledTooLongPlec()
        {
            
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
            
        }

        [Then(@"Response with error about incorrect Płeć")]
        public void ThenResponseWithErrorAboutIncorrectPlec()
        {
            
        }

        [Then(@"Response with error about incorrect Weryfikacja")]
        public void ThenResponseWithErrorAboutIncorrectWeryfikacja()
        {
            
        }

        [Then(@"Response with error about empty Adres email")]
        public void ThenResponseWithErrorAboutEmptyAdresEmail()
        {
            
        }

        [Then(@"Response with error about empty Hasło")]
        public void ThenResponseWithErrorAboutEmptyHaslo()
        {
            
        }

        [Then(@"Response with error about empty Powtórz hasło")]
        public void ThenResponseWithErrorAboutEmptyPowtorzHaslo()
        {
            
        }

        [Then(@"Response with error about empty Płeć")]
        public void ThenResponseWithErrorAboutEmptyPlec()
        {
            
        }

        [Then(@"Response with error about empty Weryfikacja")]
        public void ThenResponseWithErrorAboutEmptyWeryfikacja()
        {
            
        }

        [Then(@"Response with error about too short Adres email")]
        public void ThenResponseWithErrorAboutTooShortAdresEmail()
        {
            
        }

        [Then(@"Response with error about too short Hasło")]
        public void ThenResponseWithErrorAboutTooShortHaslo()
        {
            
        }

        [Then(@"Response with error about too short Płeć")]
        public void ThenResponseWithErrorAboutTooShortPlec()
        {
            
        }

        [Then(@"Response with error about too long Adres email")]
        public void ThenResponseWithErrorAboutTooLongAdresEmail()
        {
            
        }

        [Then(@"Response with error about too long Hasło")]
        public void ThenResponseWithErrorAboutTooLongHaslo()
        {
            
        }

        [Then(@"Response with error about too long Płeć")]
        public void ThenResponseWithErrorAboutTooLongPlec()
        {
            
        }
    }
}
