using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
   public interface IHasher
    {
        bool CompareStringToHash(string s, string hash);

        string Encrypt(string original);

       /// <summary>
        /// 16位加密
        /// </summary>
        /// <param name="orginal"></param>
        /// <returns></returns>
        string Encrypt16(string orginal);

        int SaltSize { get; set; }
    }
}
