using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class DelEntity : BaseEntity, IDelEntity
    {
        private bool mIsDelete;
        public virtual bool IsDelete
        {
            get
            {
                return mIsDelete;
            }
            set
            {
                 mIsDelete = value;
            }
        }
    }
}
