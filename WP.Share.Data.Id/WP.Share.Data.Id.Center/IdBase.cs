using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.Center
{
    internal sealed class IdBase
    {
        #region 静态方法        

        #region 方法字段
        #region 需要移动
        /// <summary>
        /// delta需要左移的数量
        /// </summary>
        private static int deltasecondleft = IdSetting.TOTAL_SIZE - IdSetting.DELTA_SIZE;

        #endregion
        private static long signalID = IdSetting.Sign << (IdSetting.TOTAL_SIZE - IdSetting.SIGNAL_SIZE);

        private static long worknodeid = IdSetting.Worknode << (IdSetting.TOTAL_SIZE - IdSetting.WORK_NODE_SIZE) >> (IdSetting.SIGNAL_SIZE+IdSetting.DELTA_SIZE);

        /// <summary>
        /// 最后时间戳  
        /// </summary>
        private static long lastTimestamp = 0L;

        /// <summary>
        /// 加锁对象  
        /// </summary>
        private static object syncRoot = new object();
        /// <summary>
        /// sequence支持最大的ID
        /// </summary>
        private static long sequencemax = (long)1L << IdSetting.SEQUENCE_SIZE - 1;

        /// <summary>
        /// sequnce 已经生成的ID数（秒级）
        /// </summary>
        private static long sequencecount = 1L;
        /// <summary>
        /// 是否检查过了配置参数
        /// </summary>
        private static bool IsChecked = true;
        #endregion

        /// <summary>
        /// 没有配置worknode 默认为2
        /// </summary>
        /// <returns></returns>
        public static long NewId()
        {
            lock (syncRoot)
            {
                #region 准备好时间和序列数
                // 获取当前时间
                long currentTime = GetTimestamp();
                if (lastTimestamp == currentTime)//同一秒中生成ID [是否有更快的比较方式？] 
                {
                    if (sequencecount == sequencemax)
                    {
                        while (true)
                        {
                            Thread.Sleep(20);
                            if (lastTimestamp != GetTimestamp())
                            {
                                currentTime = GetTimestamp();
                                lastTimestamp = currentTime;
                                sequencecount = 0L;
                                break; 
                            }
                        }
                    }
                    else
                    {
                        sequencecount += 1;
                    }
                }
                else//距离上次生成ID 下一秒了
                {
                    if (currentTime < lastTimestamp)
                    {
                        throw new ArgumentOutOfRangeException("timestamp", $"当前时间：{currentTime};上一次时间：{lastTimestamp};时间戳比上一次生成ID时时间戳还小，故异常");
                    }
                    // 如果不是同一秒了重置计数器
                    sequencecount = 0L;
                    lastTimestamp = currentTime; //把当前时间戳保存为最后生成ID的时间戳  
                }
                #endregion
                #region 组装ID 
                long Id = signalID //sign
                    | (currentTime << deltasecondleft) >> IdSetting.SIGNAL_SIZE  //Timestamp 避免N年后发生超时间的梗
                     | (worknodeid)// work nide                                  
                   | sequencecount;   //sequence
                #endregion
                return Id;
            }
        }

        /// <summary>  
        /// 生成当前时间戳  
        /// </summary>  
        /// <returns>秒</returns>  
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - IdSetting.RelativeTime).TotalSeconds;
        }

        #endregion
    }
}
