using System.Security.Cryptography;
using System.Text;
using System;

namespace PasswordManager5._0.Functions
{
    public class Userhash
    {
        public string GetUserHash(string Username, string Password, string Email)
        {
            var salt = "kwsSFTkxG5Pu4rHu";
            byte[] saltAndPwd = Encoding.UTF8.GetBytes(salt + Username + salt + Password + salt + Email + salt);
            SHA512 shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(saltAndPwd);
            string result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }
    }
}
