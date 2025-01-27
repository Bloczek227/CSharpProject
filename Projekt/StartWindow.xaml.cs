using System.IO;
using System.Windows;

namespace Projekt
{
    public partial class StartWindow : Window
    {
        private void ContinueClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(true);
            mainWindow.Show();
            Close();
        }
        private void NewGameClicked(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("save.txt"))
            {
                MainWindow mainWindow = new(false);
                mainWindow.Show();
                Close();
                return;
            }
            MessageBoxResult result = MessageBox.Show("Your current save might be lost. Are you sure?", "Confirm", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MainWindow mainWindow = new(false);
                    mainWindow.Show();
                    Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        public StartWindow()
        {
            InitializeComponent();
        }
    }
}
