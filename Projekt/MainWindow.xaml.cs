using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Projekt
{


    public delegate void ClickPerformed();
    public delegate void TickPerformed();
    public delegate void ResetPerformed();

    public partial class MainWindow : Window
    {
        private Account account;
        private List<TickUpgrade> TickUpgrs = [];
        private List<ClickUpgrade> ClickUpgrs = [];
        private Stats stats;
        private Options options;
        private event ClickPerformed ClickPerformedEvent;
        private event TickPerformed TickPerformedEvent;
        private event ResetPerformed ResetPerformedEvent;
        private void Save()
        {
            var file = new StreamWriter("save.txt");
            file.WriteLine(account.ClickMoney);
            file.WriteLine(account.TickMoney);
            file.WriteLine(account.ResetPoints);

            foreach (var tickUpgr in TickUpgrs)
            {
                file.WriteLine("{0} {1} {2}", tickUpgr.Count, tickUpgr.BoughtCount, tickUpgr.Cost);
            }
            foreach (var clickUpgr in ClickUpgrs)
            {
                file.WriteLine("{0} {1} {2}", clickUpgr.Count, clickUpgr.BoughtCount, clickUpgr.Cost);
            }

            file.WriteLine(stats.ClicksThisReset);
            file.WriteLine(stats.TotalClicks);
            file.WriteLine(stats.TicksThisReset);
            file.WriteLine(stats.TotalTicks);

            file.WriteLine(stats.ClickMoneyThisReset);
            file.WriteLine(stats.TotalClickMoney);
            file.WriteLine(stats.TickMoneyThisReset);
            file.WriteLine(stats.TotalTickMoney);

            file.WriteLine(stats.ResetsPerformed);

            file.WriteLine(options.AutoSave);
            file.Close();
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void Load()
        {
            if (!File.Exists("save.txt"))
                return;
            var file = new StreamReader("save.txt");
            account.ClickMoney = Convert.ToDouble(file.ReadLine());
            account.TickMoney = Convert.ToDouble(file.ReadLine());
            account.ResetPoints = Convert.ToDouble(file.ReadLine());
            foreach (var tickUpgr in TickUpgrs)
            {
                var values = file.ReadLine().Split(' ');
                tickUpgr.Count = Convert.ToDouble(values[0]);
                tickUpgr.BoughtCount = Convert.ToInt32(values[1]);
                tickUpgr.Cost = Convert.ToDouble(values[2]);
            }
            foreach (var clickUpgr in ClickUpgrs)
            {
                var values = file.ReadLine().Split(' ');
                clickUpgr.Count = Convert.ToDouble(values[0]);
                clickUpgr.BoughtCount = Convert.ToInt32(values[1]);
                clickUpgr.Cost = Convert.ToDouble(values[2]);
            }

            stats.ClicksThisReset = Convert.ToInt32(file.ReadLine());
            stats.TotalClicks = Convert.ToInt32(file.ReadLine());
            stats.TicksThisReset = Convert.ToInt32(file.ReadLine());
            stats.TotalTicks = Convert.ToInt32(file.ReadLine());

            stats.ClickMoneyThisReset = Convert.ToDouble(file.ReadLine());
            stats.TotalClickMoney = Convert.ToDouble(file.ReadLine());
            stats.TickMoneyThisReset = Convert.ToDouble(file.ReadLine());
            stats.TotalTickMoney = Convert.ToDouble(file.ReadLine());

            stats.ResetsPerformed = Convert.ToInt32(file.ReadLine());

            options.AutoSave = (file.ReadLine()=="True");
            file.Close();
        }
        private void LoadButtonClicked(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void CashButtonClicked(object sender, RoutedEventArgs e)
        {
            ClickPerformedEvent.Invoke();
        }
        private void TickPerformed()
        {
            TickPerformedEvent.Invoke();
        }
        private void UpgradeButtonClicked(object sender, RoutedEventArgs e)
        {
            FrameworkElement? frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                ((Upgr)frameworkElement.DataContext).Buy();
            }
        }
        private void Upgrade10ButtonClicked(object sender, RoutedEventArgs e)
        {
            FrameworkElement? frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                var uprg = ((Upgr)frameworkElement.DataContext);
                _ = Enumerable.Range(0, 10).Select(x => { uprg.Buy(); return x; }).ToList();
            }
        }
        private void ResetButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Your will lose your Coins and Upgrades. Are you sure?", "Confirm", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    account.ResetPoints = account.PotentialResetPoints;
                    ResetPerformedEvent.Invoke();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TickPerformed();
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            if(options.AutoSave)
                Save();
        }

        public void Initialize()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            stats = new();
            StatsStackPanel.DataContext = stats;
            ClickPerformedEvent += stats.ClickPerformed;
            TickPerformedEvent += stats.TickPerformed;
            ResetPerformedEvent += stats.ResetPerformed;

            options = new();
            AutoSaveCheckBox.DataContext = options;

            account = new Account(stats);
            ClickMoneyText.DataContext = account;
            TickMoneyText.DataContext = account;
            ResetStackPanel.DataContext = account;
            ResetPerformedEvent += account.ResetPerformed;

            TickUpgrs.Add(new TickUpgrade("Ticker Tier 1", account, 1.0, 1.1) { child = account });
            TickUpgrs.Add(new TickUpgrade("Ticker Tier 2", account, 100.0, 1.2) { child = TickUpgrs[0] });
            TickUpgrs.Add(new TickUpgrade("Ticker Tier 3", account, 1e5, 1.3) { child = TickUpgrs[1] });
            TickUpgrs.Add(new TickUpgrade("Ticker Tier 4", account, 1e10, 1.5) { child = TickUpgrs[2] });

            foreach (var tickUpgr in TickUpgrs)
            {
                TickPerformedEvent += tickUpgr.TickPerformed;
                ResetPerformedEvent += tickUpgr.ResetPerformed;
            }

            ClickUpgrs.Add(new ClickUpgrade("Clicker Tier 1", account, 10.0, 1.1) { child = account });
            ClickUpgrs.Add(new ClickUpgrade("Clicker Tier 2", account, 1000.0, 1.2) { child = ClickUpgrs[0] });
            ClickUpgrs.Add(new ClickUpgrade("Clicker Tier 3", account, 1e7, 1.3) { child = ClickUpgrs[1] });
            ClickUpgrs.Add(new ClickUpgrade("Clicker Tier 4", account, 1e14, 1.3) { child = ClickUpgrs[2] });

            ClickPerformedEvent += account.ClickPerformed;
            foreach (var clickUpgr in ClickUpgrs)
            {
                ClickPerformedEvent += clickUpgr.ClickPerformed;
                ResetPerformedEvent += clickUpgr.ResetPerformed;
            }

            ClickContr.ItemsSource = ClickUpgrs;
            TickContr.ItemsSource = TickUpgrs;
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }


        public MainWindow(bool load)
        {
            InitializeComponent();
            Initialize();
            if (load)
            {
                Load();
            }
        }
    }
}