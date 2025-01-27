using System.Diagnostics;

namespace Projekt
{
    public class Account : ObservableObject, IAddOnClick, IAddOnTick
    {
        private Stats Statistics;
        private double _clickMoney;
        public double ClickMoney
        {
            get { return _clickMoney; }
            set
            {
                _clickMoney = value;
                OnPropertyChanged();
            }
        }
        private double _tickMoney;
        public double TickMoney
        {
            get { return _tickMoney; }
            set
            {
                _tickMoney = value;
                OnPropertyChanged();
            }
        }

        private double _resetPoints;
        public double ResetPoints
        {
            get { return _resetPoints; }
            set
            {
                _resetPoints = value;
                OnPropertyChanged();
            }
        }

        private double _potentialResetPoints;
        public double PotentialResetPoints
        {
            get { return _potentialResetPoints; }
            set
            {
                _potentialResetPoints = value;
                OnPropertyChanged();
            }
        }
        public void CountPotentialResetPoints()
        {
            PotentialResetPoints = Math.Pow(Statistics.TotalClickMoney, 0.25) * Math.Pow(Statistics.TotalTickMoney, 0.25);
        }
        public Account(Stats stats)
        {
            ClickMoney = 0;
            TickMoney = 0;
            ResetPoints = 0;
            Statistics = stats;
            stats.TotalMoneyChangedEvent += CountPotentialResetPoints;
        }
        public void AddOnClick(double value)
        {
            double result = value * (1 + 0.01 * ResetPoints);
            ClickMoney += result;
            Statistics.TotalClickMoney += result;
            Statistics.ClickMoneyThisReset += result;
        }
        public void AddOnTick(double value)
        {
            double result = value * (1 + 0.01 * ResetPoints);
            TickMoney += result;
            Statistics.TotalTickMoney += result;
            Statistics.TickMoneyThisReset += result;
        }
        public void ClickPerformed()
        {
            ClickMoney += (1 + 0.01 * ResetPoints);
        }
        public void ResetPerformed()
        {
            ClickMoney = 0;
            TickMoney = 0;
        }
    }
}
