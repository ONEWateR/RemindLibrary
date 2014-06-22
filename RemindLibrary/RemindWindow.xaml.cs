using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemindLibrary
{
    /// <summary>
    /// RemindWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RemindWindow : Window
    {

        
        // 窗口高度微处理
        double plusHeight = 0;

        // 关闭处理
        bool cancel = true;

        // 是否显示在任务栏。默认为ture
        public static bool ShowInTask = true;
      
        // 窗口出现的y坐标
        static double y = 0;

        // 打开窗口列表。处理关闭窗口其他窗口下沉的动态效果。
        static List<Window> ws = new List<Window> { };

        

        /// <summary>
        /// 构造函数。
        /// </summary>
        private RemindWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = ShowInTask;
        }


        /// <summary>
        /// 初始化提醒窗口（不带选项）。
        /// 自定义标题，内容及图标。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="icon">图标地址</param>
        /// <param name="ocs">选项</param>
        private RemindWindow(string title, string content, string icon = "", OptionContent[] ocs = null)
        {
            InitializeComponent();
            InitialiseContent(new string[] { icon, title, content },ocs);
        }



        /// <summary>
        /// 显示提醒窗口。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="icon">图标绝对地址。建议用 pack url</param>
        /// <param name="ocs">选项内容</param>
        public static void Show(string title, string content, string icon = "", OptionContent[] ocs = null)
        {
            new RemindWindow(title, content, icon, ocs).Show();
        }
        /// <summary>
        /// 显示提醒窗口。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="icon">RemindIcon枚举，库自带图标。</param>
        /// <param name="ocs">选项内容</param>
        public static void Show(string title, string content, RemindIcon icon, OptionContent[] ocs = null)
        {
            string IconUrl = "";

            switch (icon)
            {
                case RemindIcon.DefaultIcon: IconUrl = "alarm.png"; break;
                case RemindIcon.Warning: IconUrl = "warning.png"; break;
                case RemindIcon.CA: IconUrl = "ca.png"; break;
            }

            string header = "pack://application:,,,/RemindLibrary;component/img/";
            new RemindWindow(title, content, header + IconUrl, ocs).Show();
        }

        /// <summary>
        /// 显示提醒窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="ocs">选项内容</param>
        public static void Show(string title, string content, OptionContent[] ocs = null)
        {
            new RemindWindow(title, content, "", ocs).Show();
        }

        /// <summary>
        /// 返回提醒窗口列表。
        /// </summary>
        /// <returns></returns>
        public List<Window> GetWindowsList() {
            return ws;
        }


        /// <summary>
        /// 初始化内容。
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="ops"></param>
        private void InitialiseContent(string[] ss, OptionContent[] ocs)
        {
            if (!ss[0].Equals(""))
            {

                icon.Source = new BitmapImage(new Uri(ss[0]));
            }
            else
            {
                // 默认
                icon.Source = new BitmapImage(new Uri("pack://application:,,,/RemindLibrary;component/img/alarm.png"));
            }

            title.Content = ss[1];

            AccessText at = new AccessText();
            at.TextWrapping = TextWrapping.WrapWithOverflow;
            at.Text = ss[2];
            content.Content = at;
            //MessageBox.Show(content.ActualHeight + "");

            // 初始化选项。
            if (ocs != null)
            {
                for (int i = 0; i < ocs.Length; i++)
                {
                    RemindOption ro = new RemindOption();
                    ro.init(ocs[i]);
                    if (i != 0) ro.Margin = new Thickness(0, -1, 0, 0);
                    opsGrid.Children.Add(ro);
                    plusHeight = 5;
                    ro.MouseUp += ocs[i].ClickEvent;
                    ro.Parent = this;
                }
            }

        }







        /// <summary>
        /// 窗口打开动画设置。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.Height = opsGrid.ActualHeight + content.ActualHeight + 62 + plusHeight;

            if (this.Height < 80) this.Height = 80;

            this.Topmost = true;

            ws.Add(this);

            this.Top = SystemParameters.WorkArea.Height - this.Height - 20 - y;

            // 创建缓冲
            BounceEase be = new BounceEase();
            be.Bounces = 0;
            be.Bounciness = .2;
            be.EasingMode = EasingMode.EaseIn;

            DoubleAnimation animtion = new DoubleAnimation()
            {
                From = SystemParameters.WorkArea.Width,
                To = SystemParameters.WorkArea.Width - this.Width,
                Duration = TimeSpan.FromSeconds(.8),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                EasingFunction = be
            };

            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(.8),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                EasingFunction = be
            };

            this.BeginAnimation(Window.LeftProperty, animtion);
            this.BeginAnimation(Window.OpacityProperty, animtion2);

            y += this.Height;
        }


        /// <summary>
        /// 窗口关闭动画设置。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = cancel;
            if (!cancel) return;
            // 创建缓冲
            BounceEase be = new BounceEase();
            be.Bounces = 0;
            be.Bounciness = .2;
            be.EasingMode = EasingMode.EaseIn;






            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = this.Left,
                To = this.Left + this.Width,
                Duration = TimeSpan.FromSeconds(.8),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                EasingFunction = be
            };

            DoubleAnimation animtion3 = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(.8),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                EasingFunction = be
            };



            Storyboard sb = new Storyboard();
            sb.Children.Add(animtion2);
            sb.Children.Add(animtion3);

            sb.Completed += (a, b) => { 

                cancel = false; 
                y -= this.Height;

                int n = ws.IndexOf(this);
                for (int i = n + 1; i < ws.Count; i++)
                {
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = ws[i].Top,
                        To = ws[i].Top + this.Height,
                        Duration = TimeSpan.FromSeconds(.8),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        EasingFunction = be
                    };
                    ws[i].BeginAnimation(Window.TopProperty, animtion);
                }

                this.Close(); 


            };
            Storyboard.SetTarget(animtion2, this);
            Storyboard.SetTarget(animtion3, this);

            Storyboard.SetTargetProperty(animtion2, new PropertyPath(Window.LeftProperty));
            Storyboard.SetTargetProperty(animtion3, new PropertyPath(Window.OpacityProperty));
            sb.Begin();
            
            

        }

        /// <summary>
        /// 关闭按钮事件设置。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window win = (Window)((FrameworkElement)sender).TemplatedParent;
                win.DragMove();
            }
        }

    }

    public enum RemindIcon { DefaultIcon, Warning, CA }

    public class OptionContent
    {
        public string content;
        public Uri icon;
        public MouseButtonEventHandler ClickEvent;
    }
}
