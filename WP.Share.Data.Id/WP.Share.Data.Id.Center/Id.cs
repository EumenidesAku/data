using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.Center
{
    public class Id
    {
        public static long NewId()
        {
            return IdBase.NewId();
        }

        /// <summary>
        /// 是否是当前Id生成
        /// </summary>
        /// <returns></returns>
        public static bool IsCurrentId(long id)
        {
            long sign = id >> (IdSetting.TOTAL_SIZE - IdSetting.SIGNAL_SIZE);
            if (sign != IdSetting.Sign)
            {
                return false;
            }

            long workNode = (id << (IdSetting.SIGNAL_SIZE+IdSetting.DELTA_SIZE)) >> (IdSetting.TOTAL_SIZE - IdSetting.WORK_NODE_SIZE);
            if (workNode != IdSetting.Worknode)
            {
                return false;
            }
            return true;
        }
    }
}
