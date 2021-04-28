using System;
using TechTalk.SpecFlow;

namespace RestSharpQuiz.Steps
{
    [Binding]
    [Scope(Feature = "Login module")]
    public class LoginModuleSteps
    {
        [Given(@"User registers in the system")]
        public void GivenUserRegistersInTheSystem()
        {
            
        }
        
        [Given(@"User filled email and password correctly")]
        public void GivenUserFilledEmailAndPasswordCorrectly()
        {
            
        }

        [Given(@"User didn't fill email and password")]
        public void GivenUserDidnTFillEmailAndPassword()
        {
            
        }

        [Given(@"User fills incorrect Adres email")]
        public void GivenUserFillsIncorrectAdresEmail()
        {
            
        }

        [Given(@"User fills incorrect Hasło")]
        public void GivenUserFillsIncorrectHaslo()
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

        [Given(@"User filled too long Adres email")]
        public void GivenUserFilledTooLongAdresEmail()
        {
            
        }

        [Given(@"User filled too long Hasło")]
        public void GivenUserFilledTooLongHaslo()
        {

        }

        [When(@"Request sends to API")]
        public void WhenRequestSendsToAPI()
        {

        }

        [Then(@"The server should return status 200")]
        public void ThenTheServerShouldReturnStatus200()
        {
            
        }
        
        [Then(@"Response should return token")]
        public void ThenResponseShouldReturnToken()
        {
            
        }

        [Then(@"The server should return status 400")]
        public void ThenTheServerShouldReturnStatus400()
        {

        }

        [Then(@"Response with error about missing data")]
        public void ThenResponseWithErrorAboutMissingData()
        {

        }

        [Then(@"Response with error about incorrect Adres email")]
        public void ThenResponseWithErrorAboutIncorrectAdresEmail()
        {
            
        }

        [Then(@"Response with error about incorrect Hasło")]
        public void ThenResponseWithErrorAboutIncorrectHaslo()
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

        [Then(@"Response with error about too long Adres email")]
        public void ThenResponseWithErrorAboutTooLongAdresEmail()
        {
            
        }

        [Then(@"Response with error about too long Hasło")]
        public void ThenResponseWithErrorAboutTooLongHaslo()
        {
            
        }
    }
}
