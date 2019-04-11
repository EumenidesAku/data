using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.SingleTests
{
    public class IdTestBase
    {
        /// <summary>
        /// 开启线程数
        /// </summary>
        protected const int THREAD_COUNT = 10;
        /// <summary>
        /// 单个线程的生成数
        /// </summary>
        protected const int THREAD_GENERATOR_NUMBER = 10000;

        /// <summary>
        /// 多线程缓冲集合
        /// </summary>
        protected ConcurrentBag<long> Result = new ConcurrentBag<long>();

        protected static int finish = 0;

        /// <summary>
        /// 检测集合中的Id是否都是唯一的
        /// </summary>
        /// <returns></returns>
        protected bool IsUnique()
        {
            if (Result.Count < 0)
            {
                return false;
            }
            var dt = GetProfile();
            var errs = new List<long>();
            foreach (var id in Result)
            {
                var row = dt.NewRow();
                row[0] = id;
                try
                {
                    dt.Rows.Add(row);
                }
                catch (Exception err)
                {
                    errs.Add(id);
                }
            }
            return errs.Count == 0;
        }

        protected DataTable GetProfile()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Id", typeof(long));
            dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dc };
            return dt;
        }
    }
}
