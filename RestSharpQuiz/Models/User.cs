using System;

namespace RestSharpQuiz.Models
{
    public class User
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string confirmPassword { get; set; }
        public string userGender { get; set; }
        public Nullable<bool> userVerification { get; set; }
        public string newUserEmail { get; set; }

        public User(string userEmail, string userPassword, string confirmPassword, string userGender, Nullable<bool> userVerification)
        {
            this.userEmail = userEmail;
            this.userPassword = userPassword;
            this.confirmPassword = confirmPassword;
            this.userGender = userGender;
            this.userVerification = userVerification;
        }

        public string GenerateEmail()
        {
            Random rand = new Random();

            userEmail = $"exampleEmail{rand.Next(0, 100000)}@email.com";

            return userEmail;
        }

        public string GenerateNewUserEmail()
        {
            Random rand = new Random();

            newUserEmail = $"newEmail{rand.Next(0, 100000)}@email.com";

            return newUserEmail;
        }

        public string GeneratePassword()
        {
            Random rand = new Random();

            userPassword = $"userPassword{rand.Next(0, 100)}@";

            return userPassword;
        }

        public string GenerateGender()
        {
            Random rand = new Random();
            string[] arrayofGender = { "Mężczyzna", "Kobieta", "Inna" };
            int randomNumber = rand.Next(0, 2);

            return userGender = arrayofGender[randomNumber];
        }

        public User CreateUser(User user)
        {
            if (user.userEmail == null)
                user.userEmail = GenerateEmail();
            if (user.userPassword == null)
                user.userPassword = GeneratePassword();
            if (user.confirmPassword == null)
                user.confirmPassword = userPassword;
            if (user.userGender == null)
                user.userGender = GenerateGender();
            if (user.userVerification == false)
                user.userVerification = true;

            return new User(userEmail, userPassword, confirmPassword, userGender, userVerification);
        }
    }
}