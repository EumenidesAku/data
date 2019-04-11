using WP.Share.Data.Id.Single;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace WP.Share.Data.Id.SingleTests
{
    [TestClass()]
    public class IdTests : IdTestBase
    {
        [TestMethod()]
        public void NewIdTest()
        {
            long id =Single.Id.NewId();
            Assert.IsTrue(Single.Id.IsCurrentId(id));
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


        [TestMethod()]
        public void GetSectionIdTest()
        {
            var result = Single.Id.GetSectionId(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.Fail();
        }
        
        [TestMethod()]
        public void GetSectionIdTest1()
        {
            var result = Single.Id.GetRandomId(DateTime.Now);
            Assert.Fail();
        }

        protected void IdMethod()
        {
            for (int j = 0; j < THREAD_GENERATOR_NUMBER; j++)
            {
                Result.Add(Single.Id.NewId());
            }
            Interlocked.Increment(ref finish);
        }


    }
}