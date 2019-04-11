using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WP.Share.Data.Id.Helper
{
    internal class IdParserFactory
    {
        private static IdParser Sign1IdParser = new IdParser(IdSettingRepository.Sign1IdSetting);
        private static IdParser Sign2IdParser = new IdParser(IdSettingRepository.Sign2IdSetting);
        public static IdParser GetInstance(long id)
        {
            int sign = IdParser.GetSign(id);
            switch (sign)
            {
                case 1:
                    return Sign1IdParser;
                case 2:
                    return Sign2IdParser;
                default: throw new ArgumentException("未知的协议号");
            }
        }
    }
}