using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.Center
{
    public static class IdSetting
    {
        /// <summary>
        /// 1L 表示0001
        /// 中心发号
        /// </summary>
        public static long Sign = 1L;


        /// <summary>
        /// worknode 工作号
        /// </summary>o
        public static long Worknode
        {
            get
            {
                return 1L;
            }
        }
        #region 写死不能修改的参数
        /// <summary>
        /// 总的bit数
        /// </summary>
        public const int TOTAL_SIZE = 64;

        /// <summary>
        /// signal占的bit数
        /// </summary>
        public const int SIGNAL_SIZE = 4;

        /// <summary>
        /// Timestamp
        /// delta 占的bit数
        /// </summary>
        public const int DELTA_SIZE = 32;

        /// <summary>
        /// work node 占的bit数
        /// </summary>
        public const int WORK_NODE_SIZE = 12;

        /// <summary>
        /// sequnce 占的bit数
        /// </summary>
        public const int SEQUENCE_SIZE = 16;

        /// <summary>
        /// 相对基准时间
        /// </summary>
        public static readonly DateTime RelativeTime = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion
    }
}
