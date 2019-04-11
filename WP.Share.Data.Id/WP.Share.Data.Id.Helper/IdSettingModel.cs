using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WP.Share.Data.Id.Helper
{
    public class IdSettingModel
    {
        /// <summary>
        /// 协议号
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// Timestamp
        /// delta 占的bit数
        /// </summary>
        public int DeltaSize { get; set; }

        /// <summary>
        /// work node 占的bit数
        /// </summary>
        public int WorkNodeSize { get; set; }

        /// <summary>
        /// sequnce 占的bit数
        /// </summary>
        public int SequnceSize { get; set; }
        /// <summary>
        /// 相对基准时间
        /// </summary>
        public DateTime RelativeTime { get; set; }
    }
}