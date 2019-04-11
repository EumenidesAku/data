using Microsoft.VisualStudio.TestTools.UnitTesting;
using WP.Share.Data.Id.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace WP.Share.Data.Id.Center.Tests
{
    [TestClass()]
    public class IdTests : IdTestBase
    {
        [TestMethod()]
        public void NewIdTest()
        {
            long id = Id.NewId();
            Assert.IsTrue(Id.IsCurrentId(id));
        }

        /// <summary>
        /// 单线程测试
        /// </summary>
        [TestMethod]
        public void SingleThread()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IdMethod();
            watch.Stop();
            long useTime = watch.ElapsedMilliseconds;
            Assert.IsTrue(IsUnique());
        }

        /// <summary>
        /// 多线程测试
        /// </summary>
        [TestMethod]
        public void MultiThread()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < THREAD_COUNT; i++)
            {
                var thread = new Thread(IdMethod);
                threads.Add(thread);
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var thread in threads)
            {
                thread.IsBackground = true;
                thread.Start();
            }

            while (true)
            {
                if (finish == threads.Count)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            watch.Stop();

            Assert.IsTrue(IsUnique());
        }

        protected void IdMethod()
        {
            for (int j = 0; j < THREAD_GENERATOR_NUMBER; j++)
            {
                Result.Add(Id.NewId());
            }
            Interlocked.Increment(ref finish);
        }

        /// <summary>
        /// 分析Id的组成
        /// </summary>
        [TestMethod]
        public void AnalysModel()
        {
            string mes = "AgAB8DFMHBA=";
            long id = BitConverter.ToInt64(Convert.FromBase64String(mes), 0);
            var model = new Data.Id.CenterTests.Models.IdStruct();
            model.Sign = id >> (IdSetting.TOTAL_SIZE - IdSetting.SIGNAL_SIZE);
            model.Time = IdSetting.RelativeTime.AddSeconds(id << (IdSetting.SIGNAL_SIZE) >> (IdSetting.TOTAL_SIZE - IdSetting.DELTA_SIZE));
            model.WorkNode = id << (IdSetting.SIGNAL_SIZE + IdSetting.DELTA_SIZE) >> (IdSetting.TOTAL_SIZE - IdSetting.WORK_NODE_SIZE);
            model.Sequence = id << (IdSetting.TOTAL_SIZE - IdSetting.SEQUENCE_SIZE) >> (IdSetting.TOTAL_SIZE - IdSetting.SEQUENCE_SIZE);
            Assert.IsTrue(true);
        }
    }
}