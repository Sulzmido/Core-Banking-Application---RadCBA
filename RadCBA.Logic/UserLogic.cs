using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public class UserLogic
    {
        UserRepository userRepo = new UserRepository();
        public bool IsUniqueUsername(string username)
        {
            if (userRepo.GetByUsername(username) == null)
            {
                return true;
            }
            return false;
        }
        public bool IsUniqueEmail(string email)
        {
            if (userRepo.GetByEmail(email) == null)
            {
                return true;
            }
            return false;
        }

        //public bool IsUserEmailConfirmed(int userId)
        //{
        //    var user = userRepo.GetById(userId);
        //    return user.EmailConfirmed;
        //}

        //public bool IsEmailConfirmed(string email)
        //{
        //    var users = userRepo.GetAllUsersByEmail(email);
        //    //return users.Any(u => u.EmailConfirmed == true);
        //    foreach (var user in users)
        //    {
        //        if (user.EmailConfirmed)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}        

        //public void SendPasswordToUser(string fullName, string toMail, string username, string password)
        //{
        //    var bodyFormat = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p> <h2>Thanks and have a nice day</h2>";
        //    string msgBody = string.Format(bodyFormat, "Imagine Nation", "xyz@gmail.com", "Dear " + fullName + ", an account has been created for you in our Banking application. Your username is \"" + username + "\" and your password is: \"" + password + "\". Please keep safely these details as they will be required of you to acess the application");
        //    new UtilityLogic().SendMail("xyz@gmail.com", toMail, "Your Password", msgBody);
        //}

        //public void SendEmailConfirmationTokenToUser(string callbackUrl, string toMail)
        //{
        //    var bodyFormat = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //    string msgBody = string.Format(bodyFormat, "Imagine Nation", "xyz@gmail.com", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //    new UtilityLogic().SendMail("xyz@gmail.com", toMail, "Email Confirmation" , msgBody);
        //}

        //public void SendPasswordResetTokenToUser(string callbackUrl, string toMail)
        //{
        //    var bodyFormat = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //    string msgBody = string.Format(bodyFormat, "Imagine Nation", "xyz@gmail.com", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //    new UtilityLogic().SendMail("xyz@gmail.com", toMail, "Reset Password", msgBody);
        //}

        //public User FindUser(string username, string enteredPassword)
        //{
        //    var user = userRepo.GetByUsername(username);
        //    //verifying password
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    if (VerifyHashedPassword(user.PasswordHash, enteredPassword))
        //    {
        //        return user;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
