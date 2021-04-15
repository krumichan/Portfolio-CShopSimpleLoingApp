using System;
using System.Windows.Input;

namespace LoginApp.ViewModel.Command
{
    /// <summary>
    /// Command Delegate ( can use parameters )
    /// </summary>
    /// <typeparam name="T">parameters when use execute method</typeparam>
    class ReplyCommand<T> : ICommand
    {
        private readonly Action<T> execute_ = null;
        private readonly Predicate<T> canExecute_ = null;

        public ReplyCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            execute_ = execute ?? throw new NullReferenceException("execute is null.");
            canExecute_ = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return (canExecute_ == null) ? true : canExecute_((T) parameter);
        }

        public void Execute(object parameter)
        {
            execute_((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Command Delegate
    /// </summary>
    class ReplyCommand : ICommand
    {
        private readonly Action execute_;
        private readonly Func<bool> canExecute_;

        public ReplyCommand(Action execute, Func<bool> canExecute = null)
        {
            execute_ = execute ?? throw new NullReferenceException("execute is null.");
            canExecute_ = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return (canExecute_ == null) ? true : canExecute_();
        }

        public void Execute(object parameter)
        {
            execute_();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
