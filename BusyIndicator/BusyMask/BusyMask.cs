﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BusyIndicator
{
    [TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
    [TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]
    public class BusyMask : ContentControl
    {
        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets whether the indicator is busy.")]
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy",
                typeof(bool),
                typeof(BusyMask),
                new PropertyMetadata(false, OnIsBusyChanged));

        [Description("Gets or sets whether the indicator is busy by default on startup.")]
        private bool _IsBusyATStartup;
        public bool IsBusyAtStartup
        {
            get { return _IsBusyATStartup; }
            set { _IsBusyATStartup = value; }
        }

        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets indicator content such as waiting message.")]
        public string BusyContent
        {
            get { return (string)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        public static readonly DependencyProperty BusyContentProperty =
            DependencyProperty.Register("BusyContent",
                typeof(string),
                typeof(BusyMask),
                new PropertyMetadata("Please wait..."));

        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets indicator content margin.")]
        public Thickness BusyContentMargin
        {
            get { return (Thickness)GetValue(BusyContentMarginProperty); }
            set { SetValue(BusyContentMarginProperty, value); }
        }

        public static readonly DependencyProperty BusyContentMarginProperty =
            DependencyProperty.Register("BusyContentMargin",
                typeof(Thickness),
                typeof(BusyMask),
                new PropertyMetadata(new Thickness(10)));

        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets the indicator type.")]
        public IndicatorType IndicatorType
        {
            get { return (IndicatorType)GetValue(IndicatorTypeProperty); }
            set { SetValue(IndicatorTypeProperty, value); }
        }

        public static readonly DependencyProperty IndicatorTypeProperty =
            DependencyProperty.Register("IndicatorType",
                typeof(IndicatorType),
                typeof(BusyMask),
                new PropertyMetadata(IndicatorType.Twist));

        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets the control which gets focused after the wait is over.")]
        public Control FocusAfterBusy
        {
            get { return (Control)GetValue(FocusAfterBusyProperty); }
            set { SetValue(FocusAfterBusyProperty, value); }
        }

        public static readonly DependencyProperty FocusAfterBusyProperty =
            DependencyProperty.Register("FocusAfterBusy",
                typeof(Control),
                typeof(BusyMask),
                new PropertyMetadata(null));

        [Category(nameof(BusyIndicator))]
        [Description("Gets or sets if the Button should be visible or not.")]
        public Visibility ButtonVisiblity
        {
            get { return (Visibility)GetValue(ButtonVisiblityProperty); }
            set { SetValue(ButtonVisiblityProperty, value); }
        }

        public static readonly DependencyProperty ButtonVisiblityProperty =
            DependencyProperty.Register("ButtonVisiblity",
                typeof(Visibility),
                typeof(BusyMask),
                new PropertyMetadata(Visibility.Collapsed));

        [Category(nameof(BusyIndicator))]
        [Description("Value of Progress bar.")]
        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue",
                typeof(double),
                typeof(BusyMask),
                new PropertyMetadata(0.0));

        [Category(nameof(BusyIndicator))]
        [Description("Color of Progress bar.")]
        public SolidColorBrush ProgressColor
        {
            get { return (SolidColorBrush)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.Register("ProgressColor",
                typeof(SolidColorBrush),
                typeof(BusyMask),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        static BusyMask()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyMask),
                new FrameworkPropertyMetadata(typeof(BusyMask)));
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyMask)d).OnIsBusyChanged(e);
        }

        protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                if (FocusAfterBusy != null)
                {
                    FocusAfterBusy.Dispatcher.Delay(100, (_) =>
                    {
                        FocusAfterBusy.Focus();
                    });
                }
            }

            ChangeVisualState((bool)e.NewValue);
        }

        public override void OnApplyTemplate()
        {
            if (IsBusyAtStartup) IsBusy = true;
            ChangeVisualState(IsBusyAtStartup);
        }

        protected virtual void ChangeVisualState(bool isBusyContentVisible = false)
        {
            VisualStateManager.GoToState(this, isBusyContentVisible ? "Visible" : "Hidden", true);
        }
    }
}
