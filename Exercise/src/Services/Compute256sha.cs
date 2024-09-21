using System.Security.Cryptography;
using System.Text;
using ProducerDate.src.Contracts;

namespace ProducerDate.src.Services
{
    public class Compute256sha : IHash
    {
        public string Compute(string message)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(message));
                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
