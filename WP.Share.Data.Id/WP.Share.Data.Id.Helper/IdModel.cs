using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WP.Share.Data.Id.Helper
{
    public class IdModel
    {
        /// <summary>
        /// 协议号
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// id创建时间 默认返回东八区时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 工作号
        /// </summary>
        public long WorkNode { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public long Sequence { get; set; }

    }
}