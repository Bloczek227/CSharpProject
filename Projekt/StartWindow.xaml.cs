using System.Windows;

namespace Projekt
{
    public partial class StartWindow : Window
    {
        private void ContinueClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(true);
            mainWindow.Show();
            Close();
        }
        private void NewGameClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            mainWindow.Show();
            Close();
        }
        public StartWindow()
        {
            InitializeComponent();
        }
    }
}
