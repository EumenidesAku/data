using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Id.Helper;

namespace IdHelperCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserTest();
        }

        public static void ParserTest() 
        {
            long id1 = 1161606687346458624; // 1号协议id
            long id2 = 2314478978607395428; // 2号协议id

            for (int i = 0; i < 10000; i++)
            {
                IdModel model1 = IdHelper.Parser(id1);

                IdModel model2 = IdHelper.Parser(id2);
            }
        }
    }
}
