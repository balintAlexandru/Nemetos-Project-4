using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD:Back End/Back End/Services/CommonMethod.cs
namespace MVC_Company.Services
=======
namespace Back_End.Data
>>>>>>> 4091b242df8ac9a447c8e074654863e20601e95f:Back End/Back End/Data/CommonMethod.cs
{
    public class CommonMethod
    {
        public string Key = "adef@@kfxcbv@";
        public string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
