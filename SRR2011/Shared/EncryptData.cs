/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 *	Encrypt and decode a plain text string
 *  
*/
using System;
using System.IO;
using System.Security.Cryptography;

namespace Disney.iDash.Shared
{
    public class EncryptData
    {
        private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
        byte[] rgbKey = new byte[]  {187, 207, 218, 73, 44, 135, 136, 102, 112, 23, 18, 142, 2, 177, 152, 9};
        byte[] rgbIV = new byte[] { 219, 158, 177, 19, 224, 41, 41, 108, 36, 191, 89, 189, 48, 69, 67, 43 };

        public string EncryptString(string plainText)
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(ms, TripleDes.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(ms, TripleDes.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}
