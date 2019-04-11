using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WP.Share.Data.Id.Helper
{
    public class IdHelper
    {
        /// <summary>
        /// 解析一个 id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IdModel Parser(long id)
        {
            IdParser idParser = IdParserFactory.GetInstance(id);
            IdModel model = new IdModel();
            idParser.DoParser(id, ref model);
            return model;
        }


    }
}