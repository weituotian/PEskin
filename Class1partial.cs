using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace PEskin
{
    partial class Class1
    {
        /// <summary>
        /// 选择皮肤的下拉框
        /// </summary>
        private ToolStripComboBox comboBox;

        /// <summary>
        /// 从一个窗口中获取它的菜单控件对象
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private MenuStrip searchMenu(Form form)
        {
            foreach (System.Windows.Forms.Control control in form.Controls)
            {
                if (control is MenuStrip)
                {
                    return (MenuStrip)control;
                }
            }
            return null;
        }

        /// <summary>
        /// 主窗口添加下拉框
        /// </summary>
        /// <param name="form"></param>
        private void addComboBox(Form form)
        {
            MenuStrip menu = searchMenu(form);
            
            //创建新下拉框
            comboBox = new ToolStripComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;//只允许下拉而不允许输入
            comboBox.Name = "toolStripComboBox1";
            comboBox.Size = new System.Drawing.Size(121, 25);
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.BackColor = System.Drawing.Color.Maroon;

            //遍历资源文件将其内容加入下拉框
            ResourceSet resourceSet = Resource1.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            comboBox.Items.Add("不换肤");
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = (string)entry.Key;
                //object resource = entry.Value;
                Console.WriteLine("key:" + resourceKey);
                comboBox.Items.Add(resourceKey);
                //Console.WriteLine("resource:" + resource);
            }
            comboBox.SelectedItem = curSet.styleName;

            menu.Items.Add(comboBox);//添加进菜单
        }

        /// <summary>
        /// 根据皮肤名字换肤
        /// </summary>
        /// <param name="name"></param>
        private void changeStyle(string name)
        {
            if (name == "不换肤")
            {
                setSkin(new byte[] { });
            }
            else
            {
                setSkin((byte[])Resource1.ResourceManager.GetObject(name));
            }
        }

        /// <summary>
        /// 下拉框选择更换就换肤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            changeStyle(comboBox.Text);
            //保存设置
            curSet.styleName = comboBox.Text;
            Utils.saveSettings(settingxml, curSet);
            //comboBox.Text;
            //Console.WriteLine("Silver:" + Resource1.ResourceManager.GetObject("Silver"));
        }

    }
}
