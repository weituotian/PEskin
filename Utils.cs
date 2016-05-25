using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace PEskin
{
    public class UBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return (Assembly.GetExecutingAssembly()).GetType(typeName);
        }
    }

    public class Utils
    {

        public Utils()
        {

        }

        /// <summary>
        /// 从指定文件读取设置
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public static Settings loadSettings(string pathFile)
        {
            Settings set;

            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Binder = new UBinder();
                Stream stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read, FileShare.Read);//文件流
                set = (Settings)formatter.Deserialize(stream);//反序列化
                stream.Close();//关闭流
            }
            catch (Exception)
            {
                //读取失败
                return new Settings();
            }
            return set;
        }

        /// <summary>
        /// 保存字体到xml文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="font"></param>
        public static void saveSettings(string path, Settings set)
        {
            IFormatter Fileformatter = new BinaryFormatter();
            Stream Filestream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);//文件流
            Fileformatter.Serialize(Filestream, set);//序列化
            Filestream.Close();//关闭流
        }
    }
}
