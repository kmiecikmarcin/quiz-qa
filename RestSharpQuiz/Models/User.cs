using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpQuiz.Models
{
    public class User
    {
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string confirm_password { get; set; }
        public string user_gender { get; set; }
        public bool user_verification { get; set; }

        public User(string user_email, string user_password, string confirm_password, string user_gender, bool user_verification)
        {
            this.user_email = user_email;
            this.user_password = user_password;
            this.confirm_password = confirm_password;
            this.user_gender = user_gender;
            this.user_verification = user_verification;
        }

        public string GenerateEmail()
        {
            Random rand = new Random();

            user_email = $"exampleEmail{rand.Next(0, 10000)}@email.com";

            return user_email;
        }
        public string GeneratePassword()
        {
            Random rand = new Random();

            user_password = $"userPassword{rand.Next(0, 100)}@";

            return user_password;
        }
        public string GenerateGender()
        {
            Random rand = new Random();
            string[] arrayofGender = { "Mężczyzna", "Kobieta", "Inna" };
            int randomNumber = rand.Next(0, 2);

            return user_gender = arrayofGender[randomNumber];
        }
        public User CreateUser(User user)
        {
            if(user.user_email.Length == 0)
                user.user_email = GenerateEmail();
            if (user.user_password.Length == 0)
                user.user_password = GeneratePassword();
            if (user.confirm_password.Length == 0)
                user.confirm_password = user_password;
            if (user.user_gender.Length == 0)
                user.user_gender = GenerateGender();
            if (user.user_verification == false)
                user.user_verification = true;

            user = new User(user_email, user_password, confirm_password, user_gender, user_verification);

            return user;
        }
    }
}
