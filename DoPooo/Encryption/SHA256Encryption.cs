using System.Text;
using System.Security.Cryptography;

namespace DoPooo.Encyption
{
    public class SHA256Encryption
    {
        public static string EncryptText(string text) 
        {
            if (String.IsNullOrWhiteSpace(text))
                return String.Empty;
                //throw new Exception("Text is empty for Encryption.");

            byte[] arr = Encoding.UTF8.GetBytes(text);
            SHA256 sha = SHA256.Create();
            byte [] newtext = sha.ComputeHash(arr);

            return BitConverter.ToString(newtext);
        }       
    }
}
