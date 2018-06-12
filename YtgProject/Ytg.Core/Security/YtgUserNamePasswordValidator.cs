using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Comm;

namespace Ytg.Core.Security
{
    public class YtgUserNamePasswordValidator : UserNamePasswordValidator
    {
        static string namepasswords = System.Configuration.ConfigurationManager.AppSettings["usernamepasswords"];
        //kZC5kx1IUHqxIKnqb283qsfvw6JRgW7m
        public override void Validate(string userName, string password)
        {
            var arrays = namepasswords.Split(',');
            Hasher hasher = new Hasher();
            if (arrays.Length == 0)
                throw new DataMisalignedException("");

            string cp = userName + password;
            var isComoled = false;
            foreach (var c in arrays)
            {
                if (hasher.Encrypt(c) == cp)
                {
                    isComoled = true;
                    continue;
                }
            }
            if (!isComoled)
                throw new System.Data.DataException("mysql connection exception!");
        }
    }
}
