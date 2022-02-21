using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class EncryptPassword
    {
        public static string texttoEncrypt(string Password)
        {
            //for Encrption of password
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        }
    }
}
