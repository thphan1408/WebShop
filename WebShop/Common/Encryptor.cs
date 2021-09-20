using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebShop.Common
{
    public static class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //Computer hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //Get hash result after computer it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for(int i = 0; i < result.Length; i++)
            {
                //Change it into 2 hexedecimal digits
                //For each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();

        }
    }
}