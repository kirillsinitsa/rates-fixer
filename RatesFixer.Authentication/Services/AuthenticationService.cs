using System.Text;
using System.Security.Cryptography;
using System;
using RatesFixer.Authentication.Model;
using RatesFixer.Authentication.Entities;

namespace RatesFixer.Authentication.Services
{
    public interface IAuthenticationService
    {
        UserVM AuthenticateUser(int userId, string password);
    }
 
    public class AuthenticationService : IAuthenticationService
    {
        private string connectionString;

        public AuthenticationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserVM AuthenticateUser(int userId, string clearTextPassword)
        {
            InternalUserData userData = new CredentialsService(connectionString).Get(userId);
            if ((userData == null) || !(userData.HashedPassword.Equals(CalculateHash(clearTextPassword, userId.ToString()))))
                throw new UnauthorizedAccessException("Отказано в доступе. Пожалуйта, введите корректные данные.");
 
            return new UserVM(userData.UserId, userData.Role);
        }

        public void CreateNewUser(int userId, string clearTextPassword, string role)
        {
            new CredentialsService(connectionString).Create(new InternalUserData(userId, CalculateHash(clearTextPassword, userId.ToString()), role));
        }

        public void DeleteUser(int userId)
        {
            new CredentialsService(connectionString).Delete(userId);
        }
 
        private string CalculateHash(string clearTextPassword, string salt)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
