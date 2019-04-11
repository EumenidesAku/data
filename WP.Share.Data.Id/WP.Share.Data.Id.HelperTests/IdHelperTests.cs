using Microsoft.VisualStudio.TestTools.UnitTesting;
using WP.Share.Data.Id.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace WP.Share.Data.Id.Helper.Tests
{
    [TestClass()]
    public class IdHelperTests
    {
        public int finish = 0;
        [TestMethod()]
        public void ParserTest()
        {
            long id1 = 1161606687346458624; // 1号协议id
            long id2 = 2314478978607395428; // 2号协议id
            for (int i = 0; i < 1000000; i++)
            {
                IdModel model1 = IdHelper.Parser(id1);
                Assert.AreEqual(model1.Sign, 1);
                Assert.AreEqual(model1.WorkNode, 1);
                Assert.AreEqual(model1.CreateTime, Convert.ToDateTime("2019/1/10 19:27:07"));
                Assert.AreEqual(model1.Sequence, 0);

                IdModel model2 = IdHelper.Parser(id2);
                Assert.AreEqual(model2.Sign, 2);
                Assert.AreEqual(model2.WorkNode, 11);
                Assert.AreEqual(model2.CreateTime, Convert.ToDateTime("2019/1/8 16:31:33"));
                Assert.AreEqual(model2.Sequence, 612);
            }
            Interlocked.Increment(ref finish);
        }

        [TestMethod()]
        public void ParserTest1()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 100; i++)
            {
                var thread = new Thread(ParserTest);
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
        }

        [TestMethod()]
        public void ParserTest2()
        {
            long id = 2314478951495413771;
            IdModel model = IdHelper.Parser(id);

            Assert.AreEqual(model.Sign, 2);
            Assert.AreEqual(model.WorkNode, 11);
        }

        [TestMethod()]
        public void ParserTest3()
        {
            long id = 2314535808540872585;
            IdModel model = IdHelper.Parser(id);

            Assert.AreEqual(model.Sign, 2);
            Assert.AreEqual(model.WorkNode, 0);
        }

        [TestMethod()]
        public void ParserTest4()
        {
            try
            {
                long id = 6314535808540872585;
                IdModel model = IdHelper.Parser(id);

                Assert.AreEqual(model.Sign, 2);
                Assert.AreEqual(model.WorkNode, 0);
            }catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "未知的协议号");
            }
            
        }

    }
}