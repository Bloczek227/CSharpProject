namespace Projekt
{

    public class Stats : ObservableObject
    {
        public delegate void TotalMoneyChanged();
        public event TotalMoneyChanged TotalMoneyChangedEvent;
        private int _clicksThisReset = 0;
        public int ClicksThisReset
        {
            get { return _clicksThisReset; }
            set
            {
                _clicksThisReset = value;
                OnPropertyChanged();
            }
        }
        private int _totalClicks = 0;
        public int TotalClicks
        {
            get { return _totalClicks; }
            set
            {
                _totalClicks = value;
                OnPropertyChanged();
            }
        }
        private int _ticksThisReset = 0;
        public int TicksThisReset
        {
            get { return _ticksThisReset; }
            set
            {
                _ticksThisReset = value;
                OnPropertyChanged();
            }
        }
        private int _totalTicks = 0;
        public int TotalTicks
        {
            get { return _totalTicks; }
            set
            {
                _totalTicks = value;
                OnPropertyChanged();
            }
        }


        private double _clickMoneyThisReset = 0;
        public double ClickMoneyThisReset
        {
            get { return _clickMoneyThisReset; }
            set
            {
                _clickMoneyThisReset = value;
                OnPropertyChanged();
            }
        }
        private double _totalClickMoney = 0;
        public double TotalClickMoney
        {
            get { return _totalClickMoney; }
            set
            {
                _totalClickMoney = value;
                OnPropertyChanged();
                TotalMoneyChangedEvent.Invoke();
            }
        }
        private double _tickMoneyThisReset = 0;
        public double TickMoneyThisReset
        {
            get { return _tickMoneyThisReset; }
            set
            {
                _tickMoneyThisReset = value;
                OnPropertyChanged();
            }
        }
        private double _totalTickMoney = 0;
        public double TotalTickMoney
        {
            get { return _totalTickMoney; }
            set
            {
                _totalTickMoney = value;
                OnPropertyChanged();
                TotalMoneyChangedEvent.Invoke();
            }
        }
        private int _resetsPerformed = 0;
        public int ResetsPerformed
        {
            get { return _resetsPerformed; }
            set
            {
                _resetsPerformed = value;
                OnPropertyChanged();
            }
        }

        public void ClickPerformed()
        {
            ClicksThisReset += 1;
            TotalClicks += 1;
        }
        public void TickPerformed()
        {

            TicksThisReset += 1;
            TotalTicks += 1;
        }
        public void ResetPerformed()
        {

            TicksThisReset = 0;
            ClicksThisReset = 0;
            TickMoneyThisReset = 0;
            ClickMoneyThisReset = 0;
            ResetsPerformed++;
        }
        public Stats() { }
    }
}
