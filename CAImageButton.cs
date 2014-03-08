using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RemindLibrary
{

    public class CAImageButton : Button
    {
        public static readonly DependencyProperty MyMoverBrushProperty;
        public static readonly DependencyProperty MyEnterBrushProperty;
        public Brush MyMoverBrush
        {
            get
            {
                return base.GetValue(CAImageButton.MyMoverBrushProperty) as Brush;
            }
            set
            {
                base.SetValue(CAImageButton.MyMoverBrushProperty, value);
            }
        }
        public Brush MyEnterBrush
        {
            get
            {
                return base.GetValue(CAImageButton.MyEnterBrushProperty) as Brush;
            }
            set
            {
                base.SetValue(CAImageButton.MyEnterBrushProperty, value);
            }
        }
        static CAImageButton()
        {
            CAImageButton.MyMoverBrushProperty = DependencyProperty.Register("MyMoverBrush", typeof(Brush), typeof(CAImageButton), new PropertyMetadata(null));
            CAImageButton.MyEnterBrushProperty = DependencyProperty.Register("MyEnterBrush", typeof(Brush), typeof(CAImageButton), new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CAImageButton), new FrameworkPropertyMetadata(typeof(CAImageButton)));
        }
        public CAImageButton()
        {
            base.Content = "";
        }
    }
}
