using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class JBTestSecurity
{

    //creates hashed string - for new password
    public string CreateHash(string unHashed)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.ASCII.GetBytes(unHashed);
        data = x.ComputeHash(data);
        return System.Text.Encoding.ASCII.GetString(data);
    }

    //check if two strings are matched - for login
    public bool MatchHash(string HashData, string HashUser)
    {
        HashUser = CreateHash(HashUser);
        if (HashUser == HashData)
            return true;
        else
            return false;

    }

}