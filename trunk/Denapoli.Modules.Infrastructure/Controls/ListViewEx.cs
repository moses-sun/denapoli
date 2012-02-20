using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Denapoli.Modules.Infrastructure.Command;

namespace Denapoli.Modules.Infrastructure.Controls
{
    public class ListViewEx : ListBox
    {

        public ListViewEx()
        {
            LeftScrollCommand = new ActionCommand(() =>
                                                      {
                                                          var scroller = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this, 0), 0) as ScrollViewer;
                                                          scroller.ScrollToHorizontalOffset(scroller.HorizontalOffset - 1);
                                                      });
            RightScrollCommand = new ActionCommand(() =>
                                                       {
                                                           var scroller = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this, 0), 0) as ScrollViewer;
                                                           scroller.ScrollToHorizontalOffset(scroller.HorizontalOffset + 1);
                                                       });

            TopScrollCommand = new ActionCommand(() =>
            {
                var scroller = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this, 0), 0) as ScrollViewer;
                scroller.ScrollToVerticalOffset(scroller.VerticalOffset - 10);
            });

            BottomScrollCommand = new ActionCommand(() =>
            {
                var scroller = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this, 0), 0) as ScrollViewer;
                scroller.ScrollToVerticalOffset(scroller.VerticalOffset + 10);
            });
        }



        public ICommand LeftScrollCommand
        {
            get { return (ICommand)GetValue(LeftScrollCommandProperty); }
            set { SetValue(LeftScrollCommandProperty, value); }
        }
        public static readonly DependencyProperty LeftScrollCommandProperty = DependencyProperty.Register(
          "LeftScrollCommand", typeof(ICommand), typeof(ListViewEx), new PropertyMetadata(null));



        public ICommand RightScrollCommand
        {
            get { return (ICommand)GetValue(RightScrollCommandProperty); }
            set { SetValue(RightScrollCommandProperty, value); }
        }
        public static readonly DependencyProperty RightScrollCommandProperty = DependencyProperty.Register(
          "RightScrollCommand", typeof(ICommand), typeof(ListViewEx), new PropertyMetadata(null));




        public ICommand TopScrollCommand
        {
            get { return (ICommand)GetValue(TopScrollCommandProperty); }
            set { SetValue(TopScrollCommandProperty, value); }
        }
        public static readonly DependencyProperty TopScrollCommandProperty = DependencyProperty.Register(
          "TopScrollCommand", typeof(ICommand), typeof(ListViewEx), new PropertyMetadata(null));




        public ICommand BottomScrollCommand
        {
            get { return (ICommand)GetValue(BottomScrollCommandProperty); }
            set { SetValue(BottomScrollCommandProperty, value); }
        }
        public static readonly DependencyProperty BottomScrollCommandProperty = DependencyProperty.Register(
          "BottomScrollCommand", typeof(ICommand), typeof(ListViewEx), new PropertyMetadata(null));
       
    }
}