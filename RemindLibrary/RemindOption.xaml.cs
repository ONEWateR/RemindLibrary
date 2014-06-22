using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemindLibrary
{
    /// <summary>
    /// RemindOption.xaml 的交互逻辑
    /// </summary>
    public partial class RemindOption : UserControl
    {
        public RemindOption()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// Tag格式：图标,内容
        /// 图标地址为 img/icon/
        /// </summary>
        public void init(OptionContent oc) {


            // 设置图标
            if (oc.icon != null)
                icon.Source = new BitmapImage(oc.icon);

            // 设置内容
            option.Content = oc.content;
        }

        public RemindWindow Parent { get; set; }

        private void back_MouseLeave(object sender, MouseEventArgs e)
        {
            back.Fill = Brushes.White;
        }

        private void back_MouseEnter(object sender, MouseEventArgs e)
        {
            back.Fill = new SolidColorBrush(Color.FromRgb(229, 229, 229));
        }

        private void back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            back.Fill = new SolidColorBrush(Color.FromRgb(204, 204, 204));
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            back.Fill = Brushes.White;
        }



    }
}
