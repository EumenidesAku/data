using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WP.Share.Data.Id.Helper
{
    internal class IdParser
    {
        /// <summary>
        /// 总的bit数
        /// </summary>
        public const int TOTAL_SIZE = 64;

        /// <summary>
        /// signal占的bit数
        /// </summary>
        public const int SIGNAL_SIZE = 4;

        private IdSettingModel _CurrentIdSetting = null;

        internal static int GetSign(long id)
        {
            return (int)((SignBin & id) >> TOTAL_SIZE - SIGNAL_SIZE);
        }

        public IdParser(IdSettingModel idSettting)
        {
            _CurrentIdSetting = idSettting;
        }

        public void DoParser(long id,ref IdModel model)
        {
            model.Sign = _CurrentIdSetting.Sign;
            model.Sequence = SequenceBin & id;
            model.WorkNode = (WorkNodeBin & id) >> _CurrentIdSetting.SequnceSize;
            long Timestamp = (TimestampBin & id) >> (TOTAL_SIZE - SIGNAL_SIZE - _CurrentIdSetting.DeltaSize);
            model.CreateTime = _CurrentIdSetting.RelativeTime.AddSeconds(Timestamp).AddHours(8);
        }

        #region 二进制
        private static long SignBin
        {
            get
            {
                long temp = (1L << SIGNAL_SIZE) - 1;
                return temp << TOTAL_SIZE - SIGNAL_SIZE;
            }
        }

        private long TimestampBin
        {
            get
            {
                long temp = (1L << _CurrentIdSetting.DeltaSize)-1 ;
                return temp << TOTAL_SIZE - SIGNAL_SIZE - _CurrentIdSetting.DeltaSize;
            }
        }

        private long WorkNodeBin
        {
            get
            {
                long temp = (1L << _CurrentIdSetting.WorkNodeSize) - 1;
                return temp << _CurrentIdSetting.SequnceSize;
            }
        }

        private long SequenceBin
        {
            get
            {
                return (1L << _CurrentIdSetting.SequnceSize) - 1;
            }
        }
        #endregion
    }
}