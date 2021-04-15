using System.Windows;
using System.Windows.Controls;

namespace LoginApp.ControlAssistant
{
    /// <summary>
    /// PasswordBox의 바인딩을 제공하기 위한 클래스.
    /// ( WPF는 PasswordBox의 바인딩을 보안상의 문제로 막아놓았기 때문에, 직접적인 바인딩이 불가능함. )
    /// </summary>
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached(
                "BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // 해당 이벤트(OnBoundPasswordChanged)는 PasswordBox에 붙거나,
            // BindPassword 속성이 true로 붙었을 경우에만 실행
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // PasswordBox의 변경에 따른 이벤트를 무시함으로써,
            // 재귀적인 갱신을 방지
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // BindPassword 속성이 PasswordBox에 붙었을 때,
            // PasswordChanged 이벤트의 listening을 시작한다.

            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            // 비밀번호가 갱신되고 있음을 알린다.
            SetUpdatingPassword(box, true);
            // 새로운 비밀번호를 BoundPassword에 설정 및 비밀번호의 갱신 종료를 알린다.
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPassword);
        }

        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}
