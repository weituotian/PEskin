using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PEskin
{
    /// <summary>
    /// 记录设置的类
    /// </summary>
    [Serializable]
    public class Settings
    {
        /// <summary>
        /// 是否随pe启动开启功能
        /// </summary>
        public bool bootup
        {
            get;
            set;
        }

        /// <summary>
        /// 上一次保存的皮肤
        /// </summary>
        public string styleName
        {
            get;
            set;
        }

        public Settings()
        {
            this.bootup = false;
            this.styleName = "不换肤";
        }

        public Settings(string styleName)
        {
            this.bootup = false;
            this.styleName = styleName;
        }
    }
}
