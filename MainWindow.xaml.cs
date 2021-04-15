using System.Windows;

namespace LoginApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.Core.MainViewModel();
        }
    }
}
