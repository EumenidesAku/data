using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.Single
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

        public static long GetFirstId(DateTime time, DateTimeKind kind = DateTimeKind.Local)
        {
            return IdBase.GetId(TimeToUtc(time, kind), SectionIdType.First);
        }

        public static long GetLastId(DateTime time, DateTimeKind kind = DateTimeKind.Local)
        {
            return IdBase.GetId(TimeToUtc(time, kind), SectionIdType.Last);
        }

        public static long GetRandomId(DateTime time, DateTimeKind kind = DateTimeKind.Local)
        {
            return IdBase.GetId(TimeToUtc(time, kind), SectionIdType.Random);
        }

        public static Dictionary<int,long> GetSectionId(DateTime starTime, DateTime endTime, DateTimeKind kind = DateTimeKind.Local)
        {
            Dictionary<int, long> section = new Dictionary<int, long>();
            section.Add(0, IdBase.GetId(TimeToUtc(starTime, kind), SectionIdType.First));
            section.Add(1, IdBase.GetId(TimeToUtc(endTime, kind), SectionIdType.Last));
            return section;
        }

        private static DateTime TimeToUtc(DateTime time, DateTimeKind kind)
        {
            switch (kind)
            {
                case DateTimeKind.Local:
                    time = time.AddHours(-8);
                    break;
                case DateTimeKind.Utc:
                    break;
                case DateTimeKind.Unspecified:
                    break;
                default:break;
            }
            return time;
        }
    }
}
