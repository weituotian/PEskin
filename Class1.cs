using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEskin
{
    public partial class Class1 : PEPlugin.IPEPlugin, PEPlugin.IPEPluginOption
    {

        /// <summary>
        /// 保存pe运行参数
        /// </summary>
        private PEPlugin.IPERunArgs peArgs;


        /// <summary>
        /// 获取对这个插件的描述
        /// </summary>
        public string Description
        {
            get { return ""; }
        }

        /// <summary>
        /// 获取这个插件的名称
        /// </summary>
        public string Name
        {
            get { return "pmxeditor插件"; }
        }

        /// <summary>
        /// 获取启动的选项
        /// </summary>
        public PEPlugin.IPEPluginOption Option
        {
            get { return this; }
        }

        Assembly ass;//换肤程序集
        private Type skinType;//换肤插件的类的实例
        private Object skin;//换肤插件的类的实例
        /// <summary>
        /// 换肤，根据byte[]
        /// </summary>
        /// <param name="bytes"></param>
        private void setSkin(byte[] bytes)
        {
            if (skin != null && skinType != null)
            {
                //设置skin的属性SkinStream,即可换肤
                PropertyInfo SkinStream = skinType.GetProperty("SkinStream");
                SkinStream.SetValue(skin, new System.IO.MemoryStream(bytes), null);
            }

        }

        private string settingxml;//配置文件路径
        private Settings curSet;//当前设置，内有当前皮肤名字

        /// <summary>
        /// 这个方法在插件启动时被调用。
        /// </summary>
        /// <param name="args"></param>
        public void Run(PEPlugin.IPERunArgs args)
        {
            Console.WriteLine("pe插件开始运行！");

            peArgs = args;//保存这个IPERunArgs

            //文件夹检测，没有该文件夹就创建
            string path = new FileInfo(peArgs.Host.Connector.System.HostApplicationPath).DirectoryName + @"\_data\weituotian\";
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            //配置文件路径
            settingxml = path + "skin.xml";

            //dll文件名
            string dllFileName = "IrisSkin4.dll";

            //******没有IrisSkin4.dll就写出到目录******
            if (!File.Exists(path + dllFileName))   //文件不存在
            {
                FileStream fs = new FileStream(path + dllFileName, FileMode.CreateNew, FileAccess.Write);
                byte[] buffer = Resource2.IrisSkin4;
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();
            }
            //******************

            Form mainForm = peArgs.Host.Connector.Form as Form;//主窗口

            //改变皮肤
            //加载dll
            ass = Assembly.LoadFrom(path + dllFileName);
            //创建skin实例
            skinType = ass.GetType("Sunisoft.IrisSkin.SkinEngine");
            skin = Activator.CreateInstance(skinType);
            //设置skin的属性Active
            PropertyInfo Active = skinType.GetProperty("Active");
            Active.SetValue(skin, true, null);

            //加载配置的指定的皮肤
            curSet=Utils.loadSettings(settingxml);
            changeStyle(curSet.styleName);

            #region 原始调用方法
            //Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
            //skin.SkinStream = ;
            //skin.Active = true;
            #endregion

            addComboBox(mainForm);
        }


        /// <summary>
        /// 获取对这个插件版本的描述
        /// </summary>
        public string Version
        {
            get { return "1.0 by韦驮天"; }
        }

        /// <summary>
        /// 这个插件被销毁时被调用
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 获取一个布尔值，表示插件是否随PE一起启动。取值为true或false
        /// </summary>
        public bool Bootup
        {
            get { return true; }
        }

        /// <summary>
        /// 获取一个布尔值，表示插件是否应该有菜单项。取值为true或false
        /// </summary>
        public bool RegisterMenu
        {
            get { return false; }
        }

        /// <summary>
        /// 获取一个字符串，表示插件菜单项上的文本。
        /// </summary>
        public string RegisterMenuText
        {
            get { return "插件 By 韦驮天"; }
        }
    }
}
