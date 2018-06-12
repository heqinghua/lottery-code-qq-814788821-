using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 数数据结构
    /// </summary>
    public class TreeDTO
    {

        public TreeDTO()
        {
          
        }

        /// <summary>
        /// id key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 子集合
        /// </summary>
        public bool HasChildrens { get; set; }

        /// <summary>
        /// 子集数量
        /// </summary>
        public int ChildrensCount { get; set; }
    }
}
