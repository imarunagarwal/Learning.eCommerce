using System.Security.Cryptography;
using System.Text;
using UserWebApi.DataAccessLayer.Contracts;

namespace UserWebApi.DataAccessLayer.Repository
{
    public class CoreRepository : ICoreRepository
    {
        public string GenerateHashedPassword(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
