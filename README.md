RemindLibrary ver 1.0.0
=============

WPF写的提醒窗口类库。

1、提醒窗口介绍
-----------------------------------  

这个提醒窗口是我在制作计协精灵的时候弄的。这周发现写不出一些技术心得，只能花整个下午将其提取出来，方便以后的拓展和使用。

![Image Title](http://onewater2012.farbox.com/_image/2014-03-08/21-12-35.jpg)

2、API
-----------------------------------

### RemindWindow 方法

  * | 名称 | 说明
    ------------ | ------------- | ------------
    静态/公共 | Show(string, string, string, OptionContent[])  | 显示提醒窗口。（标题，内容，图标地址，以及选项内容数组）
    静态/公共 | Show(string, string, RemindIcon, OptionContent[])  | 显示提醒窗口。（标题，内容，自带图标枚举，以及选项内容数组）
    静态/公共 | Show(string, string, OptionContent[])  | 显示提醒窗口。（标题，内容，以及选项内容数组）
        
### OptionContent 属性

  * | 名称 | 说明
    ------------ | ------------- | ------------
    公共 | content  | string。选项内容。
    公共 | icon  | Uri 。 选项文字前面的小图标地址。
    公共 | ClickEvent  | MouseButtonEventHandler。点击选项按钮触发的事件。
    

### RemindIcon 枚举
  * | 名称 | 说明
    ------------ | ------------- | ------------
    公共 | DefaultIcon  | 默认图标。
    公共 | Warning  | 警告图标。
    公共 | CA  | 计协logo
    
3、使用
-----------------------------------
### 3.1 添加 WPF 引用

添加引用 - 程序集 - 框架 - 添加[PresentationCore、PresentationFramework、System.Core、System.Xaml、WindowsBase]

### 3.2 添加 RemindLibrary.dll

添加引用 - 浏览 - 选中该 dll 即可。

### 3.3 简单例子

        ## 显示
        RemindWindow.Show(textBox1.Text, richTextBox1.Text, RemindIcon.Warning);
        
### 3.4 带选项提醒窗口的使用

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
            
### 3.5 自定义图片

            string me = @"C:\Users\ONEWateR\Documents\Visual Studio 2012\Projects\项目\RemindLibrary\WindowsFormsApplication1\bin\Debug\ow.png";
            me = "pack://siteoforigin:,,,/ow.png";  // Pack Urls

            RemindWindow.Show(textBox1.Text, richTextBox1.Text, me);
            
建议使用 pack://siteoforigin:,,,/ 格式。其次图片应该复制到输出路径，不然是找不到的 = =。当然也可以使用第一个的绝对路径。任何相对路径无效。

4、使用参考资料
-----------------------------------
[Pack Url] (http://msdn.microsoft.com/zh-cn/library/aa970069.aspx)

[Lambda 表达式](http://msdn.microsoft.com/zh-cn/library/bb397687.aspx)
