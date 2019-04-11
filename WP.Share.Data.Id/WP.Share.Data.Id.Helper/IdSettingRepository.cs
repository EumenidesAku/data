using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.Helper
{
    /// <summary>
    /// 在此配置协议解析类型
    /// </summary>
    internal class IdSettingRepository
    {
        #region 1号协议
        private static bool _IsSign1Init = false;
        private static IdSettingModel _IdSign1Settings = new IdSettingModel();
        public static IdSettingModel Sign1IdSetting
        {
            get
            {
                if (_IsSign1Init)
                {
                    return _IdSign1Settings;
                }
                _IdSign1Settings.Sign = 1;
                _IdSign1Settings.DeltaSize = 32;
                _IdSign1Settings.WorkNodeSize = 12;
                _IdSign1Settings.SequnceSize = 16;
                _IdSign1Settings.RelativeTime = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                _IsSign1Init = true;
                return _IdSign1Settings;
            }
               

        }
        #endregion

        #region  2号协议
        private static bool _IsSign2Init = false;
        private static IdSettingModel _IdSign2Settings = new IdSettingModel();
        public static IdSettingModel Sign2IdSetting
        {
            get
            {
                if (_IsSign2Init)
                {
                    return _IdSign2Settings;
                }
                _IdSign2Settings.Sign = 2;
                _IdSign2Settings.DeltaSize = 32;
                _IdSign2Settings.WorkNodeSize = 16;
                _IdSign2Settings.SequnceSize = 12;
                _IdSign2Settings.RelativeTime = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                _IsSign2Init = true;
                return _IdSign2Settings;
            }


        }
        #endregion

    }
}
