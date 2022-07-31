using System.Windows;
using System.Windows.Controls;

namespace AOEStatsApp.CustomControls
{
    public class PlaceholderTextBox : TextBox
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsEmpty), typeof(bool), typeof(PlaceholderTextBox), new PropertyMetadata(true));

        private static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;

        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            IsEmpty = string.IsNullOrEmpty(Text);
            base.OnTextChanged(e);
        }
    }
}
