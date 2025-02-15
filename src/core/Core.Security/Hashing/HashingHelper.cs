using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public static class HashingHelper
{

    // HMACSHA512 
    public static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
        using HMACSHA512 hmac = new HMACSHA512();
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        byte[] salt = hmac.Key;
        return (hash,salt);
    }

    // Kullanıcının parola hashli hali
    // AB123456

    // veri tabanındaki hash:
    // AB321456

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new HMACSHA512(passwordSalt);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //1. Yöntem

        //for(int i=0; i < computedHash.Length; i++)
        //{
        //    if (computedHash[i] != passwordHash[i])
        //    {
        //        return false;
        //    }
        //}

        //return true;


        return computedHash.SequenceEqual(passwordHash);

    }
}
