using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WP.Share.Security.Encryption.Hash;

namespace WP.Share.Data.Id.Single
{
    public static class IdSetting
    {
        /// <summary>
        /// 1L 表示0002
        /// 单机发号
        /// </summary>
        public static long Sign = 2L;


        /// <summary>
        /// worknode 工作号
        /// </summary>o
        public static long Worknode
        {
            get
            {
                if (_IsInit)
                {
                    return _WorkNode;
                }
                string worknode = ConfigurationManager.AppSettings.Get("WorkNode");
                if (string.IsNullOrEmpty(worknode))
                {
                    throw new ArgumentNullException("WorkNode", "未找到WorkNode的配置信息");
                }
                string orignal = worknode + "weipan";
                var isSame = MD5.AreSameString(orignal, WorknodeSec);
                if (!isSame)
                {
                    throw new ArgumentOutOfRangeException("WorkNode", "WorkNode和WorkNodeSec不匹配");
                }
                if (!long.TryParse(worknode, out long node))
                {
                    throw new ArgumentOutOfRangeException("WorkNode", "WorkNode无法转换为long");
                }
                _IsInit = true;
                _WorkNode = node;
                return node;
            }
        }

        private static string WorknodeSec
        {
            get
            {
                string sec = ConfigurationManager.AppSettings.Get("WorkNodeSec");
                if (string.IsNullOrEmpty(sec))
                {
                    throw new ArgumentNullException("WorkNodeSec", "未找到WorkNodeSec的配置信息");
                }
                return sec;
            }
        }

        private static bool _IsInit = false;
        private static long _WorkNode = 0;

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
        public const int WORK_NODE_SIZE = 16;

        /// <summary>
        /// sequnce 占的bit数
        /// </summary>
        public const int SEQUENCE_SIZE = 12;

        /// <summary>
        /// 相对基准时间
        /// </summary>
        internal static readonly DateTime RelativeTime = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion
    }
}
