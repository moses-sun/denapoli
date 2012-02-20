using System.Windows;
using System.Windows.Controls;

namespace Denapoli.Modules.Infrastructure.Controls
{
    public static class WidthBehavior
    {
        public static int GetWidthEx(DependencyObject obj)
        {
            return (int)obj.GetValue(WidthExProperty);
        }

        public static void SetWidthEx(DependencyObject treeViewItem, int value)
        {
            treeViewItem.SetValue(WidthExProperty, value);
        }

        public static readonly DependencyProperty WidthExProperty =
            DependencyProperty.RegisterAttached(
            "WidthEx",
            typeof(int),
            typeof(WidthBehavior),
            new UIPropertyMetadata(0, OnWidthExChanged));

        static void OnWidthExChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var t = depObj as Control;
            var val = (int)e.NewValue;
            val -= 50;
            if (val <= 0)
                val = 20;
            t.Width = val;
        }


        public static int GetHeightEx(DependencyObject obj)
        {
            return (int)obj.GetValue(HeightExProperty);
        }

        public static void SetHeightEx(DependencyObject treeViewItem, int value)
        {
            treeViewItem.SetValue(HeightExProperty, value);
        }

        public static readonly DependencyProperty HeightExProperty =
            DependencyProperty.RegisterAttached(
            "HeightEx",
            typeof(int),
            typeof(WidthBehavior),
            new UIPropertyMetadata(0, OnHeightExChanged));

        static void OnHeightExChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var t = depObj as Control;
            var val = (int)e.NewValue;
            val -= 50;
            if (val <= 0)
                val = 20;
            t.Height = val;
        }

        
    }
}