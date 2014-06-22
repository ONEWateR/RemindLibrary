using RemindLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace RemindLibraryExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 普通
            RemindIcon[] ris = new RemindIcon[] { RemindIcon.DefaultIcon, RemindIcon.Warning, RemindIcon.CA };

            RemindWindow.Show(textBox1.Text, richTextBox1.Text, ris[comboBox1.SelectedIndex]);
            RemindWindow.Show(textBox1.Text, richTextBox1.Text, RemindIcon.Warning);
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            string c = @"自定义图片只需要填写图片url即可。

建议使用 pack://siteoforigin:,,,/ + 图片名称

图片请右键属性 生成输出目录。
  ";

            
            RemindWindow.Show("说明", c, "pack://siteoforigin:,,,/ow.png");

            
        }

        private void button2_Click(object sender, EventArgs e)
        {


            OptionContent oc1 = new OptionContent
            {
                content = "Powerd By ONEWateR",
                ClickEvent = (a, b) =>
                {
                    MessageBox.Show("ONEWateR");
                }
            };

            OptionContent oc2 = new OptionContent
            {
                content = "The Second Option",
                ClickEvent = (a, b) =>
                {
                    MessageBox.Show("Second Option~!");
                    //rw2.Close();
                    ((RemindOption)a).Parent.Close();
                }
            };

            OptionContent[] ocs = new OptionContent[] { oc1, oc2 };
            RemindWindow.Show("带选项的提醒窗口", "内容......", ocs);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string me = @"C:\Users\ONEWateR\Documents\Visual Studio 2012\Projects\项目\RemindLibrary\WindowsFormsApplication1\bin\Debug\ow.png";
            me = "pack://siteoforigin:,,,/ow.png";

            RemindWindow.Show(textBox1.Text, richTextBox1.Text, me);

        }
    }
}
